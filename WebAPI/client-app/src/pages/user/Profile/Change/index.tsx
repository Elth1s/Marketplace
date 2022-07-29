import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";

import { useEffect } from "react";

import { Form, FormikProvider, useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import { ServerError } from '../../../../store/types';
import { toLowerFirstLetter } from '../../../../http_comon';

import { ProfileSchema } from "../../validation";
import { IProfile } from "../../types";

import TextFieldComponent from "../../../../components/TextField";
import Box from "@mui/material/Box";
import { BlindButton, ButtonStyled, ChangeButton } from "../../styled";

const Change = () => {
    const { GetProfile, UpdateProfile } = useActions();
    const { userInfo } = useTypedSelector((store) => store.profile);

    useEffect(() => {
        getData();
    }, []);

    const getData = async () => {
        document.title = "Change password and login";
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

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps } = formik;

    return (
        <FormikProvider value={formik} >
            <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
                <Grid container columnSpacing={21.125}>
                    <Grid container item xs={6} direction="column" rowSpacing={4.125}>
                        <Grid item sx={{ display: "inline-flex", alignItems: "flex-end" }}>
                            <Box sx={{ width: "207px" }} >
                                <TextFieldComponent
                                    type="text"
                                    label="Email"
                                    // error={errors.email}
                                    // touched={touched.email}
                                    getFieldProps={{ ...getFieldProps('email') }}
                                />
                            </Box>
                            <ChangeButton variant="outlined" color="secondary">
                                Change email
                            </ChangeButton>
                        </Grid>
                        <Grid item sx={{ display: "inline-flex", alignItems: "flex-end" }}>
                            <Box sx={{ width: "207px" }} >
                                <TextFieldComponent
                                    type="text"
                                    label="Phone"
                                    // error={errors.phone}
                                    // touched={touched.phone}
                                    getFieldProps={{ ...getFieldProps('phone') }}
                                />
                            </Box>
                            <ChangeButton variant="outlined" color="secondary" >
                                Change phone
                            </ChangeButton>
                        </Grid>
                        <ButtonStyled fullWidth variant="contained" color="primary" sx={{ mt: "97px" }}>
                            Change password
                        </ButtonStyled>
                    </Grid>
                    <Grid container item xs={6} rowSpacing={1.625}
                        alignContent="flex-start"
                        direction="column">
                        <Grid item xs>
                            <Typography variant="subtitle1">Ви можете зв'язати свій особистий кабінет з обліковими записами соціальних мереж, щоб надалі входити на сайт, як користувач Facebook або Google.</Typography>
                        </Grid>
                        <Grid item xs>
                            <Box sx={{
                                display: "flex",
                                justifyContent: "space-between",
                                alignItems: "center",
                                paddingBottom: "13px",
                                borderBottom: "1px solid",
                            }}>
                                <Box sx={{
                                    display: "inline-flex",
                                    alignItems: "center",
                                }}>
                                    <svg width="25" height="25" viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
                                        <path d="M22.714 10.4599H21.875V10.4167H12.5V14.5834H18.387C17.5281 17.0089 15.2203 18.75 12.5 18.75C9.04842 18.75 6.24998 15.9516 6.24998 12.5C6.24998 9.04848 9.04842 6.25004 12.5 6.25004C14.0932 6.25004 15.5427 6.85108 16.6463 7.83285L19.5927 4.8865C17.7323 3.15264 15.2437 2.08337 12.5 2.08337C6.74738 2.08337 2.08331 6.74744 2.08331 12.5C2.08331 18.2526 6.74738 22.9167 12.5 22.9167C18.2526 22.9167 22.9166 18.2526 22.9166 12.5C22.9166 11.8016 22.8448 11.1198 22.714 10.4599Z" fill="#FFC107" />
                                        <path d="M3.28436 7.6516L6.70676 10.1615C7.6328 7.86879 9.87551 6.25004 12.5 6.25004C14.0932 6.25004 15.5427 6.85108 16.6463 7.83285L19.5927 4.8865C17.7323 3.15264 15.2437 2.08337 12.5 2.08337C8.49895 2.08337 5.02915 4.34223 3.28436 7.6516Z" fill="#FF3D00" />
                                        <path d="M12.5 22.9167C15.1906 22.9167 17.6354 21.887 19.4839 20.2125L16.2599 17.4844C15.1789 18.3064 13.8581 18.7511 12.5 18.75C9.79062 18.75 7.4901 17.0224 6.62344 14.6115L3.22656 17.2286C4.95052 20.6021 8.45156 22.9167 12.5 22.9167Z" fill="#4CAF50" />
                                        <path d="M22.7141 10.4599H21.875V10.4166H12.5V14.5833H18.387C17.9761 15.7377 17.2361 16.7464 16.2583 17.4849L16.2599 17.4838L19.4839 20.2119C19.2557 20.4192 22.9167 17.7083 22.9167 12.5C22.9167 11.8015 22.8448 11.1198 22.7141 10.4599Z" fill="#1976D2" />
                                    </svg>
                                    <Typography variant="subtitle1" sx={{ fontWeight: "500", marginLeft: "11px" }}>Google</Typography>
                                </Box>
                                <svg width="18" height="13" viewBox="0 0 18 13" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <path d="M16.8818 1L5.96305 11.9187L1 6.95566" stroke="#0E7C3A" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
                                </svg>
                            </Box>
                        </Grid>
                        <Grid item xs>
                            <Box sx={{
                                display: "flex",
                                justifyContent: "space-between",
                                alignItems: "center",
                                paddingBottom: "13px",
                                borderBottom: "1px solid",
                            }}>
                                <Box sx={{
                                    display: "inline-flex",
                                    alignItems: "center",
                                }}>
                                    <svg width="25" height="25" viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
                                        <path d="M13.9115 22.9166V13.4135H17.101L17.5792 9.71037H13.9125V7.34579C13.9125 6.27287 14.2094 5.54266 15.7469 5.54266L17.7083 5.54162V2.22912C16.7593 2.12867 15.8054 2.07999 14.851 2.08329C12.0229 2.08329 10.0875 3.80933 10.0875 6.97912V9.71037H6.88959V13.4135H10.0875V22.9166H13.9115Z" fill="#2447FF" />
                                    </svg>
                                    <Typography variant="subtitle1" sx={{ fontWeight: "500", marginLeft: "11px" }}>Facebook</Typography>
                                </Box>
                                <BlindButton variant="text" color="primary" sx={{ textTransform: "none" }}>Bind</BlindButton>
                            </Box>
                        </Grid>
                        <ButtonStyled fullWidth variant="outlined" color="secondary" sx={{ mt: "63px" }}>
                            Remove profile
                        </ButtonStyled>
                    </Grid>
                </Grid>
            </Form>
        </FormikProvider>
    );
}

export default Change;