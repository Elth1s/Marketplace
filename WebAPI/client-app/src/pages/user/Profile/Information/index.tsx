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

import { useTranslation } from "react-i18next";
import { LoadingButton } from "@mui/lab";
import { Button, MenuItem, useTheme } from "@mui/material";
import { TextFieldFirstStyle } from "../../../../components/TextField/styled";

const Information = () => {
    const { t } = useTranslation();
    const { palette } = useTheme();

    const { GetProfile, UpdateProfile, GetCountries, GetCitiesByCountry, GetGenders } = useActions();
    const { userInfo } = useTypedSelector((store) => store.profile);
    const { countries } = useTypedSelector((store) => store.country);
    const { cityForSelect } = useTypedSelector((store) => store.city);
    const { genders } = useTypedSelector((store) => store.profile);

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

    useEffect(() => {
        document.title = `${t("pages.user.personalInformation.tabs.personalInfo")}`;

        getData();
    }, [formik.values.countryId]);

    const getData = async () => {
        try {
            await GetProfile();
            await GetGenders();
            await GetCountries();
            if (userInfo.countryId != "")
                await GetCitiesByCountry(+userInfo.countryId)
        } catch (ex) {
        }
    }

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps, setFieldValue } = formik;

    return (
        <FormikProvider value={formik} >
            <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
                <Grid container columnSpacing="180px">
                    <Grid container item xs={6} rowSpacing="25px">
                        <Grid item xs={12}>
                            <TextFieldFirstStyle
                                fullWidth
                                variant="standard"
                                type="text"
                                label={t("validationProps.firstName")}
                                {...getFieldProps('firstName')}
                                error={Boolean(touched.firstName && errors.firstName)}
                                helperText={touched.firstName && errors.firstName}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextFieldFirstStyle
                                fullWidth
                                variant="standard"
                                type="text"
                                label={t("validationProps.secondName")}
                                {...getFieldProps('secondName')}
                                error={Boolean(touched.secondName && errors.secondName)}
                                helperText={touched.secondName && errors.secondName}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextFieldFirstStyle
                                select
                                fullWidth
                                variant="standard"
                                label={t("validationProps.gender")}
                                error={Boolean(touched.genderId && errors.genderId)}
                                helperText={touched.genderId && errors.genderId}
                                {...getFieldProps('genderId')}
                            >
                                {genders && genders.map((item) =>
                                    <MenuItem key={item.id} value={item.id}>{item.name}</MenuItem>
                                )}
                            </TextFieldFirstStyle>
                        </Grid>
                        <Grid item xs={12} height="73px" />
                    </Grid>
                    <Grid container item xs={6} rowSpacing="25px">
                        <Grid item xs={12}>
                            <TextFieldFirstStyle
                                select
                                fullWidth
                                variant="standard"
                                label={t("validationProps.country")}
                                error={Boolean(touched.countryId && errors.countryId)}
                                helperText={touched.countryId && errors.countryId}
                                {...getFieldProps('countryId')}
                                onChange={(event) => {
                                    setFieldValue("countryId", event.target.value)
                                    setFieldValue("cityId", "")
                                }}
                                SelectProps={{
                                    MenuProps: {
                                        PaperProps: {
                                            sx: {
                                                maxHeight: "200px"
                                            }
                                        }
                                    }
                                }}
                            >
                                {countries && countries.map((item) =>
                                    <MenuItem key={item.id} value={item.id}>{item.name}</MenuItem>
                                )}
                            </TextFieldFirstStyle>
                        </Grid>
                        <Grid item xs={12}>
                            <TextFieldFirstStyle
                                select
                                fullWidth
                                variant="standard"
                                label={t("validationProps.city")}
                                error={Boolean(touched.cityId && errors.cityId)}
                                helperText={touched.cityId && errors.cityId}
                                {...getFieldProps('cityId')}
                                SelectProps={{
                                    MenuProps: {
                                        PaperProps: {
                                            sx: {
                                                maxHeight: "200px"
                                            }
                                        }
                                    }
                                }}
                            >
                                {cityForSelect && cityForSelect.map((item) =>
                                    <MenuItem key={item.id} value={item.id}>{item.name}</MenuItem>
                                )}
                            </TextFieldFirstStyle>
                        </Grid>
                        <Grid item xs={12}>
                            <TextFieldFirstStyle
                                fullWidth
                                variant="standard"
                                type="text"
                                label={t("validationProps.address")}
                                {...getFieldProps('address')}
                                error={Boolean(touched.address && errors.address)}
                                helperText={touched.address && errors.address}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextFieldFirstStyle
                                fullWidth
                                variant="standard"
                                type="text"
                                label={t("validationProps.postalCode")}
                                {...getFieldProps('postalCode')}
                                error={Boolean(touched.postalCode && errors.postalCode)}
                                helperText={touched.postalCode && errors.postalCode}
                            />
                        </Grid>
                    </Grid>
                    <Grid item xs={12} sx={{ display: "flex", justifyContent: "space-between", mt: "40px" }}>
                        <LoadingButton
                            color="primary"
                            variant="contained"
                            loading={isSubmitting}
                            type="submit"
                            sx={{
                                width: "auto",
                                px: "32.5px",
                                py: "12.5px",
                                textTransform: "none",
                                borderRadius: "10px",
                                fontSize: "20px",
                                height: "50px",
                                "&:hover": { background: palette.primary.main }
                            }}
                        >
                            {t("pages.user.personalInformation.saveChanges")}
                        </LoadingButton>
                        <Button
                            color="secondary"
                            variant="outlined"
                            sx={{
                                width: "auto",
                                px: "15.5px",
                                py: "11.5px",
                                textTransform: "none",
                                borderRadius: "10px",
                                fontSize: "20px",
                                height: "50px",
                                border: "1px solid #0E7C3A"
                            }}
                        >
                            {t("pages.user.personalInformation.deleteProfile")}
                        </Button>
                    </Grid>
                </Grid>
            </Form>
        </FormikProvider >
    );
}

export default Information;