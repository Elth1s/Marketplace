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
import { GoogleLogin, GoogleLoginResponse } from 'react-google-login';
import { ReactFacebookLoginInfo } from "react-facebook-login";
import FacebookLogin from 'react-facebook-login/dist/facebook-login-render-props'
import { useTranslation } from "react-i18next";
import { Button, useTheme } from "@mui/material";

const Change = () => {
    const { t } = useTranslation();
    const { palette } = useTheme();

    const { GetProfile, UpdateProfile, GoogleConnect, FacebookConnect } = useActions();
    const { userInfo } = useTypedSelector((store) => store.profile);

    useEffect(() => {
        const start = () => {
            gapi.client.init({
                clientId: process.env.REACT_APP_GOOGLE_CLIENT_ID as string,
                scope: 'Profile',
            });
        }
        gapi.load('client:auth2', start);

        getData();
    }, []);

    const getData = async () => {
        document.title = `${t("pages.user.personalInformation.tabs.changePasswordAndLogin")}`;
        await GetProfile();
    }

    const responseFacebook = async (res: ReactFacebookLoginInfo) => {
        try {
            await FacebookConnect({ token: res.accessToken });

        } catch (exception) {
            const serverError = exception as ServerError;
        }
    }


    const handleGoogleSignIn = async (res: GoogleLoginResponse | any) => {
        try {
            await GoogleConnect({ token: res.tokenId });
        } catch (exception) {
            const serverError = exception as ServerError;
        }
    }

    const onGoogleLoginFailure = (res: any) => {
        console.log('Login Failed:', res);
    };

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
                <Grid container columnSpacing="30px">
                    <Grid container item xs={7} rowSpacing="45px">
                        <Grid item xs={12} sx={{ display: "inline-flex", alignItems: "flex-end" }}>
                            <Box sx={{ width: "207px" }} >
                                <TextFieldComponent
                                    type="text"
                                    label="Email"
                                    error={errors.email}
                                    touched={touched.email}
                                    getFieldProps={{ ...getFieldProps('email') }}
                                />
                            </Box>
                            <ChangeButton variant="outlined" color="secondary" sx={{ ml: "20px" }}>
                                {t("pages.user.personalInformation.changeEmail")}
                            </ChangeButton>
                            <ChangeButton variant="outlined" color="secondary" sx={{ ml: "30px" }}>
                                {t("pages.user.personalInformation.confirmEmail")}
                            </ChangeButton>
                        </Grid>
                        <Grid item xs={12} sx={{ display: "inline-flex", alignItems: "flex-end" }}>
                            <Box sx={{ width: "207px" }} >
                                <TextFieldComponent
                                    type="text"
                                    label="Phone"
                                    error={errors.phone}
                                    touched={touched.phone}
                                    getFieldProps={{ ...getFieldProps('phone') }}
                                />
                            </Box>
                            <ChangeButton variant="outlined" color="secondary" sx={{ ml: "20px" }}>
                                {t("pages.user.personalInformation.changePhone")}
                            </ChangeButton>
                            <ChangeButton variant="outlined" color="secondary" sx={{ ml: "30px" }}>
                                {t("pages.user.personalInformation.confirmPhone")}
                            </ChangeButton>
                        </Grid>
                    </Grid>
                    <Grid container item xs={5} rowSpacing="25px">
                        <Grid item xs={12}>
                            <Typography variant="subtitle1" color="inherit">
                                {t("pages.user.personalInformation.connectText")}
                            </Typography>
                        </Grid>
                        <Grid item xs={12}>
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
                                    <img src={icon_color_google} alt="google-icon" />
                                    <Typography variant="subtitle1" color="inherit" sx={{ fontWeight: "500", marginLeft: "11px" }}>Google</Typography>
                                </Box>
                                {userInfo.isGoogleConnected ?
                                    <img src={check} alt="check-icon" />
                                    :
                                    <GoogleLogin
                                        clientId={process.env.REACT_APP_GOOGLE_CLIENT_ID as string}
                                        buttonText="Sign In"
                                        onSuccess={handleGoogleSignIn}
                                        onFailure={onGoogleLoginFailure}
                                        cookiePolicy='single_host_origin'
                                        prompt='select_account'
                                        render={renderProps => (
                                            <BlindButton variant="text"
                                                onClick={renderProps.onClick} color="primary" sx={{ textTransform: "none" }}>
                                                {t("pages.user.personalInformation.connect")}
                                            </BlindButton>
                                        )}
                                    />
                                }
                            </Box>
                        </Grid>
                        <Grid item xs={12}>
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
                                    <img src={icon_color_facebook} alt="facebook-icon" />
                                    <Typography variant="subtitle1" color="inherit" sx={{ fontWeight: "500", marginLeft: "11px" }}>Facebook</Typography>
                                </Box>

                                {userInfo.isFacebookConnected ?
                                    <img src={check} alt="check-icon" />
                                    : <FacebookLogin
                                        appId={process.env.REACT_APP_FACEBOOK_APP_ID as string}
                                        callback={responseFacebook}
                                        render={renderProps => (
                                            <BlindButton variant="text" onClick={renderProps.onClick} color="primary" sx={{ textTransform: "none" }}>
                                                {t("pages.user.personalInformation.connect")}
                                            </BlindButton>
                                        )} />

                                }
                            </Box>
                        </Grid>
                    </Grid>
                    <Grid item xs={12} sx={{ display: "flex", justifyContent: "space-between", mt: "45px" }}>
                        <Button
                            color="primary"
                            variant="contained"
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
                            {t("pages.user.personalInformation.changePassword")}
                        </Button>
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
        </FormikProvider>
    );
}

export default Change;