import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';

import { useState } from "react";
import { useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";

import { countryValidation } from "../validation";
import { ICountry } from "../types";
import { ServerError } from '../../../../store/types';

import DialogComponent from '../../../../components/Dialog';
import TextFieldComponent from "../../../../components/TextField";

const CountryCreate = () => {
    const [open, setOpen] = useState(false);

    const { CreateCountry, GetCountries } = useActions();

    const item: ICountry = {
        name: "",
        code: ""
    };

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const onHandleSubmit = async (values: ICountry) => {
        try {
            await CreateCountry(values);
            // await GetCountries();
            handleClickClose();
            resetForm();
        } catch (ex) {
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
        validationSchema: countryValidation,
        onSubmit: onHandleSubmit
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps, resetForm } = formik;

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

            dialogTitle="Create"
            dialogBtnConfirm="Create"

            dialogContent={
                <Grid container rowSpacing={2}>
                    <Grid item xs={12} >
                        <TextFieldComponent
                            type="text"
                            label="Name"
                            error={errors.name}
                            touched={touched.name}
                            getFieldProps={{ ...getFieldProps('name') }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="Code"
                            error={errors.code}
                            touched={touched.code}
                            getFieldProps={{ ...getFieldProps('code') }}
                        />
                    </Grid>
                </Grid>
            }
        />
    )
}

export default CountryCreate;