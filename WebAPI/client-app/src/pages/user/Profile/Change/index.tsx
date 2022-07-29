import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";

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
import { check, icon_color_facebook, icon_color_google } from "../../../../assets/icons";

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
                                    error={errors.email}
                                    touched={touched.email}
                                    getFieldProps={{ ...getFieldProps('email') }}
                                />
                            </Box>
                            <ChangeButton variant="outlined" color="secondary" sx={{ ml: "20px"}}>
                                Change email
                            </ChangeButton>
                            <ChangeButton variant="outlined" color="secondary" sx={{ ml: "30px"}}>
                                Confirm email
                            </ChangeButton>
                        </Grid>
                        <Grid item sx={{ display: "inline-flex", alignItems: "flex-end" }}>
                            <Box sx={{ width: "207px" }} >
                                <TextFieldComponent
                                    type="text"
                                    label="Phone"
                                    error={errors.phone}
                                    touched={touched.phone}
                                    getFieldProps={{ ...getFieldProps('phone') }}
                                />
                            </Box>
                            <ChangeButton variant="outlined" color="secondary" sx={{ ml: "20px"}}>
                                Change phone
                            </ChangeButton>
                            <ChangeButton variant="outlined" color="secondary" sx={{ ml: "30px"}}>
                                Confirm phone
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
                                    <img src={icon_color_google} alt="google-icon"/>
                                    <Typography variant="subtitle1" sx={{ fontWeight: "500", marginLeft: "11px" }}>Google</Typography>
                                </Box>
                                <img src={check} alt="check-icon"/>
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
                                    <img src={icon_color_facebook} alt="facebook-icon"/>
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