import Grid from '@mui/material/Grid';

import { FC, useState } from "react";
import { useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { validationFields } from "../validation";
import { ServerError, UpdateProps } from '../../../../store/types';

import DialogComponent from '../../../../components/Dialog';
import { Edit } from '@mui/icons-material';
import { toLowerFirstLetter } from '../../../../http_comon';
import AutocompleteComponent from '../../../../components/Autocomplete';
import { TextFieldFirstStyle } from '../../../../components/TextField/styled';




const Update: FC<UpdateProps> = ({ id, afterUpdate }) => {
    const [open, setOpen] = useState(false);

    const { GetCityById, GetCountries, UpdateCity } = useActions();

    const { selectedCity } = useTypedSelector((store) => store.city);
    const { countries } = useTypedSelector((store) => store.country);

    const handleClickOpen = async () => {
        setOpen(true);
        await GetCityById(id);
        await GetCountries();
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const formik = useFormik({
        initialValues: selectedCity,
        validationSchema: validationFields,
        enableReinitialize: true,
        onSubmit: async (values, { setFieldError }) => {
            try {
                await UpdateCity(id, values);
                afterUpdate()
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
    });

    const { errors, touched, isSubmitting, handleSubmit, getFieldProps, setFieldValue } = formik;
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

            dialogTitle="Update city"
            dialogBtnConfirm="Update"

            dialogContent={
                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <TextFieldFirstStyle
                            fullWidth
                            variant="standard"
                            autoComplete="name"
                            type="text"
                            label="Name"
                            {...getFieldProps('name')}
                            error={Boolean(touched.name && errors.name)}
                            helperText={touched.name && errors.name}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <AutocompleteComponent
                            label="Country"
                            name="countryId"
                            error={errors.countryId}
                            touched={touched.countryId}
                            options={countries}
                            getOptionLabel={(option) => option.name}
                            isOptionEqualToValue={(option, value) => option?.id === value.id}
                            defaultValue={countries.find(value => value.id === selectedCity.countryId)}
                            onChange={(e, value) => { setFieldValue("countryId", value?.id) }}
                        />
                    </Grid>
                </Grid>
            }
        />
    )
}

export default Update;