import {
    Box,
    DialogActions,
    DialogContent,
    DialogTitle,
    Grid,
    IconButton,
    InputAdornment,
    Typography
} from '@mui/material';
import {
    FC,
    useState
} from 'react'
import { Close, VisibilityOffOutlined, VisibilityOutlined } from '@mui/icons-material';
import { Form, FormikProvider, useFormik } from 'formik';
import { useGoogleReCaptcha } from 'react-google-recaptcha-v3';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';

import { useActions } from '../../../hooks/useActions';

import { toLowerFirstLetter } from '../../../http_comon';

import { ServerError } from '../../../store/types';
import { ILoginModel } from '../types';
import { LogInSchema } from '../validation';

import { TextFieldFirstStyle } from '../../../components/TextField/styled';
import { LoadingButtonStyle } from '../../../components/LoadingButton/styled';

import GoogleExternalLogin from '../../../components/Google';
import FacebookExternalLogin from '../../../components/Facebook';

import { black_eye, eye_off } from "../../../assets/icons";

interface Props {
    changeDialog: any,
    forgotPasswordOpen: any
}

const SignInDialog: FC<Props> = ({ changeDialog, forgotPasswordOpen }) => {
    const { LoginUser, AuthDialogChange, GetBasketItems } = useActions();
    const { executeRecaptcha } = useGoogleReCaptcha();

    const navigate = useNavigate();

    const [showPassword, setShowPassword] = useState(false);
    const loginModel: ILoginModel = { emailOrPhone: '', password: '' };

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
                navigate("/");
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
            <DialogTitle sx={{ py: "36px" }}>
                <Typography fontSize="30px" align="center" lineHeight="38px">
                    Sign In
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
                                    label="Email address or phone"
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
                                    label="Password"
                                    {...getFieldProps('password')}
                                    InputProps={{
                                        endAdornment: (
                                            <InputAdornment position="end">
                                                <IconButton sx={{ pt: "0px", pb: "0px", mb: "8px", "&:hover": { background: "transparent" }, "&& .MuiTouchRipple-child": { backgroundColor: "transparent" } }} onClick={handleShowPassword}>
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
                            <Grid item xs={12} sx={{ width: "100%", display: "flex", justifyContent: "space-between" }} >
                                <Typography variant='subtitle1' lineHeight="25px" color="#7e7e7e" sx={{ cursor: "pointer" }}
                                    onClick={() => {
                                        AuthDialogChange();
                                        forgotPasswordOpen();
                                    }}
                                >
                                    Forgot password?
                                </Typography>
                                <Typography variant='subtitle1' lineHeight="25px" sx={{ cursor: "pointer" }}
                                    onClick={changeDialog}
                                >
                                    Don't have an account?
                                </Typography>
                            </Grid>
                            <Grid item xs={12} display="flex" justifyContent="center" >
                                <Box sx={{ width: "98px", height: "14px", borderBottom: "2px solid #000" }} />
                                <Typography variant="h5" sx={{ padding: "0 7px" }}>or</Typography>
                                <Box sx={{ width: "98px", height: "14px", borderBottom: "2px solid #000" }} />
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
                            Sign In
                        </LoadingButtonStyle>
                    </DialogActions>
                </Form>
            </FormikProvider>
        </>
    )
}

export default SignInDialog;