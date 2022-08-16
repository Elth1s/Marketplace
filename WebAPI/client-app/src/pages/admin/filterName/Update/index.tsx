import Grid from '@mui/material/Grid';
import IconButton from '@mui/material/IconButton';
import EditIcon from '@mui/icons-material/Edit';

import { FC, useState } from "react";
import { useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { validationFields } from "../validation";
import { IFilterName } from "../types";
import { ServerError } from '../../../../store/types';

import DialogComponent from '../../../../components/Dialog';
import TextFieldComponent from '../../../../components/TextField';
import { toLowerFirstLetter } from '../../../../http_comon';
import { Edit } from '@mui/icons-material';
import AutocompleteComponent from '../../../../components/Autocomplete';

interface Props {
    id: number,
    afterUpdate: any
}

const FilterUpdate: FC<Props> = ({ id, afterUpdate }) => {
    const [open, setOpen] = useState(false);

    const { GetByIdFilterName, GetFilterGroups, UpdateFilterName } = useActions();

    const { selectedFilterName } = useTypedSelector((store) => store.filterName);
    const { filterGroups } = useTypedSelector((store) => store.filterGroup);
    const { units } = useTypedSelector((store) => store.unit);

    const handleClickOpen = async () => {
        setOpen(true);
        await GetFilterGroups();
        await GetByIdFilterName(id);
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const onHandleSubmit = async (values: IFilterName) => {
        try {
            await UpdateFilterName(id, values);
            afterUpdate();
            handleClickClose();
        }
        catch (ex) {
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
        initialValues: selectedFilterName,
        validationSchema: validationFields,
        enableReinitialize: true,
        onSubmit: onHandleSubmit
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps, setFieldValue } = formik;

    return (
        <DialogComponent
            open={open}
            handleClickClose={handleClickClose}
            button={
                <Edit onClick={() => handleClickOpen()} />
            }

            formik={formik}
            isSubmitting={isSubmitting}
            handleSubmit={handleSubmit}

            dialogTitle="Update filter name"
            dialogBtnConfirm="Update"

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
                            defaultValue={filterGroups.find(value => value.id === selectedFilterName.filterGroupId)}
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
                            isOptionEqualToValue={(option, value) => option.id === value.id}
                            defaultValue={units.find(value => value.id === selectedFilterName.unitId)}
                            onChange={(e, value) => { setFieldValue("unitId", value?.id || null) }}
                        />
                    </Grid>
                </Grid>
            }
        />
    )
}

export default FilterUpdate;