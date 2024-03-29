import {
    Box,
    DialogActions,
    DialogContent,
    DialogTitle,
    Grid,
    IconButton,
    InputAdornment,
    Typography,
    useTheme
} from '@mui/material';
import { Close, VisibilityOffOutlined, VisibilityOutlined } from '@mui/icons-material';

import * as Yup from 'yup';
import { FC, useState } from 'react'
import { Form, FormikProvider, useFormik } from 'formik';
import { useGoogleReCaptcha } from 'react-google-recaptcha-v3';
import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import { toast } from 'react-toastify';

import { useActions } from '../../../hooks/useActions';

import { toLowerFirstLetter } from '../../../http_comon';

import { ServerError } from '../../../store/types';
import { ILoginModel } from '../types';

import { TextFieldFirstStyle } from '../../../components/TextField/styled';
import { LoadingButtonStyle } from '../../../components/LoadingButton/styled';

import GoogleExternalLogin from '../../../components/Google';
import FacebookExternalLogin from '../../../components/Facebook';

import { black_eye, eye_light, eye_off, eye_off_light } from "../../../assets/icons";

interface Props {
    changeDialog: any,
    forgotPasswordOpen: any
}

const SignInDialog: FC<Props> = ({ changeDialog, forgotPasswordOpen }) => {
    const { t } = useTranslation();
    const { palette } = useTheme();

    const { LoginUser, AuthDialogChange, GetBasketItems } = useActions();
    const { executeRecaptcha } = useGoogleReCaptcha();

    const navigate = useNavigate();

    const [showPassword, setShowPassword] = useState(false);
    const loginModel: ILoginModel = { emailOrPhone: '', password: '' };

    const LogInSchema = Yup.object().shape({
        emailOrPhone: Yup.string().required().label(t('validationProps.emailOrPhone')),
        password: Yup.string().required().label(t('validationProps.password'))
    });

    const formik = useFormik({
        initialValues: loginModel,
        validationSchema: LogInSchema,
        onSubmit: async (values, { setFieldError }) => {
            if (!executeRecaptcha) {
                //toast.error("Captcha validation error");
                return;
            }
            const reCaptchaToken = await executeRecaptcha();
            try {
                await LoginUser(values, reCaptchaToken);
                AuthDialogChange();
                await GetBasketItems();
                //toast.success('Login Success!');
            }
            catch (exeption) {
                const serverErrors = exeption as ServerError;
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
                let message = "Login failed! \n";
                if (serverErrors.status === 400)
                    message += "Validation failed.";
                if (serverErrors.status === 401)
                    message += "The user with the entered data does not exist.";
                //toast.error(message, { position: "top-right" });
            }

        }
    });

    const handleShowPassword = () => {
        setShowPassword((show) => !show);
    };

    const { errors, touched, isSubmitting, handleSubmit, getFieldProps } = formik;

    return (
        <>
            <DialogTitle color="inherit" sx={{ py: "36px" }}>
                <Typography color="inherit" fontSize="30px" align="center" lineHeight="38px">
                    {t('pages.signIn.title')}
                </Typography>
                <IconButton
                    color="inherit"
                    onClick={AuthDialogChange}
                    sx={{
                        position: 'absolute',
                        right: 20,
                        top: 36,
                        borderRadius: "12px"
                    }}
                >
                    <Close />
                </IconButton>
            </DialogTitle>
            <FormikProvider value={formik} >
                <Form autoComplete="off" noValidate onSubmit={handleSubmit} >
                    <DialogContent sx={{ width: "450px", mx: "auto", mt: "34px", p: 0 }}>
                        <Grid container rowSpacing={5}>
                            <Grid item xs={12} >
                                <TextFieldFirstStyle
                                    fullWidth
                                    variant="standard"
                                    type="text"
                                    label={t('validationProps.emailOrPhone')}
                                    {...getFieldProps('emailOrPhone')}
                                    error={Boolean(touched.emailOrPhone && errors.emailOrPhone)}
                                    helperText={touched.emailOrPhone && errors.emailOrPhone}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextFieldFirstStyle
                                    fullWidth
                                    variant="standard"
                                    type={showPassword ? 'text' : 'password'}
                                    label={t('validationProps.password')}
                                    {...getFieldProps('password')}
                                    InputProps={{
                                        endAdornment: (
                                            <InputAdornment position="end">
                                                <IconButton sx={{ pt: "0px", pb: "0px", mb: "8px", "&:hover": { background: "transparent" }, "&& .MuiTouchRipple-child": { backgroundColor: "transparent" } }} onClick={handleShowPassword}>
                                                    {showPassword
                                                        ? <img
                                                            style={{ width: "30px", height: "30px" }}
                                                            src={palette.mode != "dark" ? eye_off : eye_off_light}
                                                            alt="icon"
                                                        />
                                                        : <img
                                                            style={{ width: "30px", height: "30px" }}
                                                            src={palette.mode != "dark" ? black_eye : eye_light}
                                                            alt="icon"
                                                        />}
                                                </IconButton>
                                            </InputAdornment>
                                        )
                                    }}
                                    error={Boolean(touched.password && errors.password)}
                                    helperText={touched.password && errors.password}
                                />
                            </Grid>
                            <Grid item xs={12} sx={{ width: "100%", display: "flex", justifyContent: "space-between" }} >
                                <Typography variant='subtitle1' lineHeight="25px" color="#7e7e7e" sx={{ cursor: "pointer" }}
                                    onClick={() => {
                                        AuthDialogChange();
                                        forgotPasswordOpen();
                                    }}
                                >
                                    {t('pages.signIn.forgotPassword')}
                                </Typography>
                                <Typography variant='subtitle1' color="inherit" lineHeight="25px" sx={{ cursor: "pointer" }}
                                    onClick={changeDialog}
                                >
                                    {t('pages.signIn.DoNotHaveAnAccount')}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} display="flex" justifyContent="center" >
                                <Box sx={{ width: "98px", height: "14px", borderBottom: `2px solid ${palette.mode == "dark" ? "#FFF" : "#000"}` }} />
                                <Typography variant="h5" color="inherit" sx={{ padding: "0 7px" }}>{t('pages.signIn.or')}</Typography>
                                <Box sx={{ width: "98px", height: "14px", borderBottom: `2px solid ${palette.mode == "dark" ? "#FFF" : "#000"}` }} />
                            </Grid>
                            <Grid item xs={12} display="flex" justifyContent="center" >
                                <GoogleExternalLogin />
                                <FacebookExternalLogin />
                            </Grid>
                        </Grid>
                    </DialogContent>
                    <DialogActions sx={{ pt: "30px", pb: "26px", px: "32px" }}>
                        <LoadingButtonStyle
                            color="secondary"
                            variant="contained"
                            loading={isSubmitting}
                            type="submit"
                            sx={{ width: "auto", px: "74px", py: "15px", ml: "auto" }}
                        >
                            {t('pages.signIn.btnSignIn')}
                        </LoadingButtonStyle>
                    </DialogActions>
                </Form>
            </FormikProvider>
        </>
    )
}

export default SignInDialog;