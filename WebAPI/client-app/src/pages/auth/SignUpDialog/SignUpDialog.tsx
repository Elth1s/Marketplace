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
import { IRegisterModel } from '../types';
import { SignUpSchema } from '../validation';

import { TextFieldFirstStyle } from '../../../components/TextField/styled';

import GoogleExternalLogin from '../../../components/Google';
import FacebookExternalLogin from '../../../components/Facebook';

import { black_eye, eye_off } from "../../../assets/icons";
import { LoadingButtonStyle } from '../../../components/LoadingButton/styled';

interface Props {
    changeDialog: any
}

const SignUpDialog: FC<Props> = ({ changeDialog }) => {
    const { t } = useTranslation();
    const { palette } = useTheme();

    const { RegisterUser, AuthDialogChange, GetBasketItems } = useActions();
    const { executeRecaptcha } = useGoogleReCaptcha();

    const navigate = useNavigate();

    const [showPassword, setShowPassword] = useState(false);
    const registerModel: IRegisterModel = { firstName: '', secondName: '', emailOrPhone: '', password: '' };

    const passwordRegExp = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_]).{8,}$/

    const SignUpSchema = Yup.object().shape({
        firstName: Yup.string().min(3).max(50).required().label(t('validationProps.firstName')),
        secondName: Yup.string().min(3).max(75).required().label(t('validationProps.secondName')),
        emailOrPhone: Yup.string().required().label(t('validationProps.emailOrPhone')),
        password: Yup.string().matches(passwordRegExp, 'Password is not valid').required().label(t('validationProps.password')),
    });

    const formik = useFormik({
        initialValues: registerModel,
        validationSchema: SignUpSchema,
        onSubmit: async (values, { setFieldError }) => {
            if (!executeRecaptcha) {
                toast.error("Captcha validation error");
                return;
            }
            const reCaptchaToken = await executeRecaptcha();
            try {
                await RegisterUser(values, reCaptchaToken);
                AuthDialogChange();
                await GetBasketItems();
                //toast.success('Sign up success!');
            }
            catch (exception) {
                const serverErrors = exception as ServerError;
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
                let message = "Sign up failed! \n";
                if (serverErrors.status === 400)
                    message += "Validation failed.";
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
                    {t('pages.signUp.title')}
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
                            <Grid item xs={12}>
                                <TextFieldFirstStyle
                                    fullWidth
                                    variant="standard"
                                    autoComplete="firstName"
                                    type="text"
                                    label={t('validationProps.firstName')}
                                    {...getFieldProps('firstName')}
                                    error={Boolean(touched.firstName && errors.firstName)}
                                    helperText={touched.firstName && errors.firstName}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextFieldFirstStyle
                                    fullWidth
                                    variant="standard"
                                    autoComplete="secondName"
                                    type="text"
                                    label={t('validationProps.secondName')}
                                    {...getFieldProps('secondName')}
                                    error={Boolean(touched.secondName && errors.secondName)}
                                    helperText={touched.secondName && errors.secondName}
                                />
                            </Grid>
                            <Grid item xs={12} >
                                <TextFieldFirstStyle
                                    fullWidth
                                    variant="standard"
                                    autoComplete="emailOrPhone"
                                    type="text"
                                    label={t('validationProps.emailOrPhone')}
                                    {...getFieldProps('emailOrPhone')}
                                    error={Boolean(touched.emailOrPhone && errors.emailOrPhone)}
                                    helperText={touched.emailOrPhone && errors.emailOrPhone}
                                />
                            </Grid>
                            <Grid item xs={12} >
                                <TextFieldFirstStyle
                                    fullWidth
                                    variant="standard"
                                    autoComplete="password"
                                    type={showPassword ? 'text' : 'password'}
                                    label={t('validationProps.password')}
                                    {...getFieldProps('password')}
                                    InputProps={{
                                        endAdornment: (
                                            <InputAdornment position="end">
                                                <IconButton sx={{ pt: "0px", pb: "0px", mb: "8px", "&:hover": { background: "transparent" }, "&& .MuiTouchRipple-child": { backgroundColor: "transparent" } }} onClick={handleShowPassword} >
                                                    {showPassword
                                                        ? <img
                                                            style={{ width: "30px", height: "30px" }}
                                                            src={eye_off}
                                                            alt="icon"
                                                        />
                                                        : <img
                                                            style={{ width: "30px", height: "30px" }}
                                                            src={black_eye}
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
                            <Grid item xs={12} sx={{ width: "100%", display: "flex", justifyContent: "end" }} >
                                <Typography variant='subtitle1' color="inherit" lineHeight="25px" sx={{ cursor: "pointer" }}
                                    onClick={changeDialog}>
                                    {t('pages.signUp.haveAnAccount')}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} display="flex" justifyContent="center" >
                                <Box sx={{ width: "98px", height: "14px", borderBottom: `2px solid  ${palette.mode == "dark" ? "#FFF" : "#000"}` }} />
                                <Typography variant="h5" color="inherit" sx={{ padding: "0 7px" }}>{t('pages.signUp.or')}</Typography>
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
                            sx={{ width: "auto", px: "51px", py: "15px", ml: "auto", fontSize: "20px", lineHeight: "25px", textTransform: "none" }}
                        >
                            {t('pages.signUp.btnSignUp')}
                        </LoadingButtonStyle>
                    </DialogActions>
                </Form>
            </FormikProvider>
        </>
    )
}

export default SignUpDialog;