import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';

import { FC, useState } from "react";
import { useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { validationFields } from "../validation";
import { IFilterName } from "../types";
import { CreateProps, ServerError } from '../../../../store/types';

import DialogComponent from '../../../../components/Dialog';
import TextFieldComponent from "../../../../components/TextField";
import { toLowerFirstLetter } from '../../../../http_comon';
import AutocompleteComponent from '../../../../components/Autocomplete';

const FilterCreate: FC<CreateProps> = ({ afterCreate }) => {
    const [open, setOpen] = useState(false);

    const { CreateFilterName, GetFilterGroups, GetUnits } = useActions();

    const { filterGroups } = useTypedSelector((store) => store.filterGroup);
    const { units } = useTypedSelector((store) => store.unit);

    const item: IFilterName = {
        englishName: "",
        ukrainianName: "",
        filterGroupId: 0,
        unitId: null,
    }

    const handleClickOpen = async () => {
        await GetFilterGroups();
        await GetUnits();
        setOpen(true);
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const onHandleSubmit = async (values: IFilterName) => {
        try {
            await CreateFilterName(values);
            afterCreate();
            resetForm();
            handleClickClose();
        } catch (ex) {
            const serverErrors = ex as ServerError;
            if (serverErrors.errors)
                Object.entries(serverErrors.errors).forEach(([key, value]) => {
                    if (Array.isArray(value)) {
                        let message = "";
                        value.forEach((item) => {
                            message += `${item} `;
                        });
                        setFieldError(toLowerFirstLetter(key), message);
                    }
                });
        }
    }

    const formik = useFormik({
        initialValues: item,
        validationSchema: validationFields,
        onSubmit: onHandleSubmit
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps, resetForm, setFieldValue } = formik;

    return (
        <DialogComponent
            open={open}
            handleClickClose={handleClickClose}
            button={
                <Button
                    variant="contained"
                    sx={{
                        my: 2,
                        px: 4,
                    }}
                    onClick={handleClickOpen}
                >
                    Create
                </Button>
            }

            formik={formik}
            isSubmitting={isSubmitting}
            handleSubmit={handleSubmit}

            dialogTitle="Create filter name"
            dialogBtnConfirm="Create"

            dialogContent={
                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="English Name"
                            error={errors.englishName}
                            touched={touched.englishName}
                            getFieldProps={{ ...getFieldProps('englishName') }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="Ukrainian Name"
                            error={errors.ukrainianName}
                            touched={touched.ukrainianName}
                            getFieldProps={{ ...getFieldProps('ukrainianName') }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <AutocompleteComponent
                            label="Filter group"
                            name="filterGroupId"
                            error={errors.filterGroupId}
                            touched={touched.filterGroupId}
                            options={filterGroups}
                            getOptionLabel={(option) => option.name}
                            isOptionEqualToValue={(option, value) => option?.id === value.id}
                            defaultValue={undefined}
                            onChange={(e, value) => { setFieldValue("filterGroupId", value?.id) }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <AutocompleteComponent
                            label="Unit measure"
                            name="unitId"
                            error={errors.unitId}
                            touched={touched.unitId}
                            options={units}
                            getOptionLabel={(option) => option.measure}
                            isOptionEqualToValue={(option, value) => option?.id === value.id}
                            defaultValue={undefined}
                            onChange={(e, value) => { setFieldValue("unitId", value?.id) }}
                        />
                    </Grid>
                </Grid>
            }
        />
    )
}

export default FilterCreate;