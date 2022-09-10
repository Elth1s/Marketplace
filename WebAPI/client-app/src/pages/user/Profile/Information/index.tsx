import Grid from "@mui/material/Grid";

import { useEffect } from "react";

import { Form, FormikProvider, useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import { ServerError } from '../../../../store/types';
import { toLowerFirstLetter } from '../../../../http_comon';

import { ProfileSchema } from "../../validation";
import { IProfile } from "../../types";

import TextFieldComponent from "../../../../components/TextField";
import SelectComponent from "../../../../components/Select";
import DatePickerComponent from "../../../../components/DatePicker";

import { ButtonStyled } from "../../styled";

const Information = () => {
    const { GetProfile, UpdateProfile, GetCountries, GetCitiesByCountry, GetGenders } = useActions();
    const { userInfo } = useTypedSelector((store) => store.profile);
    const { countries } = useTypedSelector((store) => store.country);
    const { cityForSelect } = useTypedSelector((store) => store.city);

    useEffect(() => {
        getData();
    }, []);

    const getData = async () => {
        document.title = "Personal information";
        await GetProfile();
    }

    const onHandleSubmit = async (values: IProfile) => {
        try {
            await UpdateProfile(values);
        } catch (ex) {
            const serverErrors = ex as ServerError;
            if (serverErrors.errors) {
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
    }

    const formik = useFormik({
        initialValues: userInfo,
        validationSchema: ProfileSchema,
        enableReinitialize: true,
        onSubmit: onHandleSubmit
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps, setFieldValue } = formik;

    return (
        <FormikProvider value={formik} >
            <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
                <Grid container columnSpacing={27.5}>
                    <Grid container item xs={6} rowSpacing={2.5} direction="column">
                        <Grid item xs>
                            <TextFieldComponent
                                type="text"
                                label="First Name"
                                error={errors.firstName}
                                touched={touched.firstName}
                                getFieldProps={{ ...getFieldProps('firstName') }}
                            />
                        </Grid>
                        <Grid item xs>
                            <TextFieldComponent
                                type="text"
                                label="Second Name"
                                error={errors.secondName}
                                touched={touched.secondName}
                                getFieldProps={{ ...getFieldProps('secondName') }}
                            />
                        </Grid>
                        <Grid item xs>
                            <SelectComponent
                                label="Gender"
                                items={[{ id: 1, name: "test1" }, { id: 2, name: "test2" }]}
                                error={errors.genderId}
                                touched={touched.genderId}
                                getFieldProps={{ ...getFieldProps('genderId') }}
                            />
                        </Grid>
                        <Grid item xs>
                            {/* <DatePickerComponent
                                label={"birthDate"}
                                error={errors.birthDate}
                                touched={touched.birthDate}
                                onChange={(value) => { setFieldValue('birthDate', value) }}
                                {...getFieldProps('birthDate')}
                                label={t('validationProps.dateStart')}
                                {...getFieldProps('dateStart')}
                                error={errors.dateStart}
                                touched={touched.dateStart}
                                onChange={(value) => { setFieldValue('dateStart', value) }}
                            /> */}
                        </Grid>
                        <Grid item xs>
                            {/* <SelectComponent
                                label="Language of communication"
                                items={[{ id: 1, name: "test1" }, { id: 2, name: "test2" }]}
                                error={errors.languageOfCommunication}
                                touched={touched.languageOfCommunication}
                                getFieldProps={{ ...getFieldProps('languageOfCommunication') }}
                            /> */}
                        </Grid>
                        <ButtonStyled fullWidth variant="contained" color="primary" sx={{ mt: "95px" }}>
                            Save Change
                        </ButtonStyled>
                    </Grid>
                    <Grid container item xs={6} rowSpacing={2.5} direction="column">
                        <Grid item xs>
                            <SelectComponent
                                label="Country"
                                items={[{ id: 1, name: "test1" }, { id: 2, name: "test2" }]}
                                error={errors.countryId}
                                touched={touched.countryId}
                                getFieldProps={{ ...getFieldProps('countryId') }}
                            />
                        </Grid>
                        <Grid item xs>
                            <TextFieldComponent
                                type="text"
                                label="Address"
                                error={errors.address}
                                touched={touched.address}
                                getFieldProps={{ ...getFieldProps('address') }}
                            />
                        </Grid>
                        <Grid item xs>
                            <SelectComponent
                                label="City"
                                items={[{ id: 1, name: "test1" }, { id: 2, name: "test2" }]}
                                error={errors.cityId}
                                touched={touched.cityId}
                                getFieldProps={{ ...getFieldProps('cityId') }}
                            />
                        </Grid>
                        <Grid item xs>
                            <TextFieldComponent
                                type="text"
                                label="Postal Code"
                                error={errors.postalCode}
                                touched={touched.postalCode}
                                getFieldProps={{ ...getFieldProps('postalCode') }}
                            />
                        </Grid>
                        <ButtonStyled fullWidth variant="outlined" color="secondary" sx={{ mt: "188px" }}>
                            Remove profile
                        </ButtonStyled>
                    </Grid>
                </Grid>
            </Form>
        </FormikProvider>
    );
}

export default Information;