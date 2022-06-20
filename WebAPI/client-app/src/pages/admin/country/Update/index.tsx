import Grid from '@mui/material/Grid';
import IconButton from '@mui/material/IconButton';
import EditIcon from '@mui/icons-material/Edit';

import { FC, useState } from "react";
import { useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { validationFields } from "../validation";
import { ICountry, ICountryUpdatePage } from "../types";
import { ServerError } from '../../../../store/types';

import DialogComponent from '../../../../components/Dialog';
import TextFieldComponent from '../../../../components/TextField';

const CountryUpdate: FC<ICountryUpdatePage> = ({ id }) => {
    const [open, setOpen] = useState(false);

    const { GetCountries, GetByIdCountry, UpdateCountry } = useActions();
    const { countryInfo } = useTypedSelector((store) => store.country);

    const item: ICountry = {
        name: countryInfo.name,
    }

    const handleClickOpen = async () => {
        setOpen(true);
        await GetByIdCountry(id);
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const onHandleSubmit = async (values: ICountry) => {
        try {
            await UpdateCountry(countryInfo.id, values);
            await GetCountries();
            handleClickClose();
            resetForm();
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
                        setFieldError(key.toLowerCase(), message);
                    }
                });
        }
    }

    const formik = useFormik({
        initialValues: item,
        validationSchema: validationFields,
        enableReinitialize: true,
        onSubmit: onHandleSubmit
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps, resetForm } = formik;

    return (
        <DialogComponent
            open={open}
            handleClickClose={handleClickClose}
            button={
                <IconButton
                    aria-label="edit"
                    onClick={handleClickOpen}
                >
                    <EditIcon />
                </IconButton>
            }

            formik={formik}
            isSubmitting={isSubmitting}
            handleSubmit={handleSubmit}

            dialogTitle="Create"
            dialogBtnCancel="Close"
            dialogBtnConfirm="Create"

            dialogContent={
                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="Name"
                            error={errors.name}
                            touched={touched.name}
                            getFieldProps={{ ...getFieldProps('name') }}
                        />
                    </Grid>
                </Grid>
            }
        />
    )
}

export default CountryUpdate;