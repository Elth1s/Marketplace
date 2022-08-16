import Grid from '@mui/material/Grid';
import IconButton from '@mui/material/IconButton';
import EditIcon from '@mui/icons-material/Edit';

import { FC, useState } from "react";
import { useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { validationFields } from "../validation";
import { IFilterValue } from "../types";
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

const FilterValueUpdate: FC<Props> = ({ id, afterUpdate }) => {
    const [open, setOpen] = useState(false);

    const { GetByIdFilterValue, GetFilterNames, UpdateFilterValue } = useActions();

    const { selectedFilterValue } = useTypedSelector((store) => store.filterValue);
    const { filterNames } = useTypedSelector((store) => store.filterName);

    const handleClickOpen = async () => {
        setOpen(true);
        await GetFilterNames();
        await GetByIdFilterValue(id);
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const onHandleSubmit = async (values: IFilterValue) => {
        try {
            await UpdateFilterValue(id, values);
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
        initialValues: selectedFilterValue,
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

            dialogTitle="Update filter value"
            dialogBtnConfirm="Update"

            dialogContent={
                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="English Value"
                            error={errors.englishValue}
                            touched={touched.englishValue}
                            getFieldProps={{ ...getFieldProps('englishValue') }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="Ukrainian Value"
                            error={errors.ukrainianValue}
                            touched={touched.ukrainianValue}
                            getFieldProps={{ ...getFieldProps('ukrainianValue') }}
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
                            defaultValue={filterNames.find(value => value.id === selectedFilterValue.filterNameId)}
                            onChange={(e, value) => { setFieldValue("filterNameId", value?.id) }}
                        />
                    </Grid>
                </Grid>
            }
        />
    )
}

export default FilterValueUpdate;