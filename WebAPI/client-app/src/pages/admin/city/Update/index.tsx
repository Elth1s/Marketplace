import Grid from '@mui/material/Grid';
import IconButton from '@mui/material/IconButton';
import EditIcon from '@mui/icons-material/Edit';

import { FC, useState } from "react";
import { useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { validationFields } from "../validation";
import { ICity, ICityUpdatePage } from "../types";
import { ServerError } from '../../../../store/types';

import DialogComponent from '../../../../components/Dialog';
import SelectComponent from '../../../../components/Select';
import TextFieldComponent from '../../../../components/TextField';

const CityUpdate: FC<ICityUpdatePage> = ({ id }) => {
    const [open, setOpen] = useState(false);

    const { GetByIdCity, GetCountries, UpdateCity, GetCities } = useActions();

    const { cityInfo } = useTypedSelector((store) => store.city);
    const { countries } = useTypedSelector((store) => store.country);

    const item: ICity = {
        name: cityInfo.name,
        countryId: countries.find(n => n.name === cityInfo.countryName)?.id || ''
    }

    const handleClickOpen = async () => {
        setOpen(true);
        await GetCountries();
        await GetByIdCity(id);
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const onHandleSubmit = async (values: ICity) => {
        console.log("data", values);
        try {
            await UpdateCity(cityInfo.id, values);
            await GetCities();
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

            dialogTitle="Update"
            dialogBtnCancel="Close"
            dialogBtnConfirm="Update"

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
                    <Grid item xs={12}>
                        <SelectComponent
                            label="City"
                            items={countries}
                            error={errors.countryId}
                            touched={touched.countryId}
                            getFieldProps={{ ...getFieldProps('countryId') }}
                        />
                    </Grid>
                </Grid>
            }
        />
    )
}

export default CityUpdate;