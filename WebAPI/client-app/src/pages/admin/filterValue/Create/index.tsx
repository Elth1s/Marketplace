import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';

import { FC, useState } from "react";
import { useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { validationFields } from "../validation";
import { IFilterValue } from "../types";
import { CreateProps, ServerError } from '../../../../store/types';

import DialogComponent from '../../../../components/Dialog';
import TextFieldComponent from "../../../../components/TextField";
import { toLowerFirstLetter } from '../../../../http_comon';
import AutocompleteComponent from '../../../../components/Autocomplete';

const FilterValueCreate: FC<CreateProps> = ({ afterCreate }) => {
    const [open, setOpen] = useState(false);

    const { CreateFilterValue, GetFilterNames } = useActions();

    const { filterNames } = useTypedSelector((store) => store.filterName);

    const item: IFilterValue = {
        value: '',
        min: "",
        max: "",
        filterNameId: 0,
    }

    const handleClickOpen = async () => {
        await GetFilterNames();
        setOpen(true);
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const onHandleSubmit = async (values: IFilterValue) => {
        try {
            await CreateFilterValue(values);
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

            dialogTitle="Create filter value"
            dialogBtnConfirm="Create"

            dialogContent={
                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="Value"
                            error={errors.value}
                            touched={touched.value}
                            getFieldProps={{ ...getFieldProps('value') }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="Min"
                            error={errors.min}
                            touched={touched.min}
                            getFieldProps={{ ...getFieldProps('min') }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="Max"
                            error={errors.max}
                            touched={touched.max}
                            getFieldProps={{ ...getFieldProps('max') }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <AutocompleteComponent
                            label="Filter name"
                            name="filterNameId"
                            error={errors.filterNameId}
                            touched={touched.filterNameId}
                            options={filterNames}
                            getOptionLabel={(option) => option.name}
                            isOptionEqualToValue={(option, value) => option?.id === value.id}
                            defaultValue={undefined}
                            onChange={(e, value) => { setFieldValue("filterNameId", value?.id) }}
                        />
                    </Grid>
                </Grid>
            }
        />
    )
}

export default FilterValueCreate;