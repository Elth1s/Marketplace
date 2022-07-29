import { Close } from '@mui/icons-material'
import { Dialog, DialogActions, DialogContent, DialogTitle, Grid, IconButton, InputAdornment, Typography } from '@mui/material'
import { Form, FormikProvider, useFormik } from 'formik'
import { FC, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { black_eye, eye_off } from '../../../assets/icons'
import { DialogStyle } from '../../../components/Dialog/styled'
import { LoadingButtonStyle } from '../../../components/LoadingButton/styled'
import { TextFieldFirstStyle } from '../../../components/TextField/style'
import { useActions } from '../../../hooks/useActions'
import { toLowerFirstLetter } from '../../../http_comon'
import { ServerError } from '../../../store/types'
import { ICreateShop } from './types'
import { CreateShopSchema } from './validation'

interface Props {
    dialogOpen: boolean,
    dialogClose: any
}

const CreateShopDialog: FC<Props> = ({ dialogOpen, dialogClose }) => {
    const { RegisterShop } = useActions();

    const navigate = useNavigate();

    const [showPassword, setShowPassword] = useState(false);
    const shopModel: ICreateShop = { name: '', siteUrl: '', fullName: '', email: '', password: '' };

    const formik = useFormik({
        initialValues: shopModel,
        validationSchema: CreateShopSchema,
        onSubmit: async (values, { setFieldError }) => {
            try {
                await RegisterShop(values);
                dialogClose();
                navigate("/");
                //toast.success('Register shop success!');
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
                let message = "Register shop failed! \n";
                // toast.error(message, { position: "top-right" });
            }

        }
    });

    const handleShowPassword = () => {
        setShowPassword((show) => !show);
    };

    const { errors, touched, isSubmitting, handleSubmit, getFieldProps } = formik;

    return (
        <DialogStyle
            open={dialogOpen}
            onClose={dialogClose}
            aria-describedby="alert-dialog-slide-description"
        >
            <DialogTitle sx={{ py: "36px" }}>
                <Typography fontSize="30px" align="center" lineHeight="38px">
                    Register Shop
                </Typography>
                <IconButton
                    color="inherit"
                    onClick={dialogClose}
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
                    <DialogContent sx={{ width: "450px", mx: "auto", mt: "28px", p: 0 }}>
                        <Typography variant="h4" lineHeight="23px">
                            Store information
                        </Typography>
                        <Grid container rowSpacing="30px"
                            sx={{
                                mb: "80px",
                                mt: "10px",
                                "&>*:nth-of-type(1)": {
                                    pt: "0px"
                                }
                            }}
                        >
                            <Grid item xs={12} >
                                <TextFieldFirstStyle
                                    fullWidth
                                    variant="standard"
                                    type="text"
                                    label="Name"
                                    {...getFieldProps('name')}
                                    error={Boolean(touched.name && errors.name)}
                                    helperText={touched.name && errors.name}
                                />
                            </Grid>
                            <Grid item xs={12} >
                                <TextFieldFirstStyle
                                    fullWidth
                                    variant="standard"
                                    type="text"
                                    label="Site Url"
                                    {...getFieldProps('siteUrl')}
                                    error={Boolean(touched.siteUrl && errors.siteUrl)}
                                    helperText={touched.siteUrl && errors.siteUrl}
                                />
                            </Grid>
                        </Grid>
                        <Typography variant="h4" lineHeight="23px">
                            Seller information
                        </Typography>
                        <Grid container rowSpacing="30px"
                            sx={{
                                mt: "10px",
                                "&>*:nth-of-type(1)": {
                                    pt: "0px"
                                }
                            }}
                        >
                            <Grid item xs={12} >
                                <TextFieldFirstStyle
                                    fullWidth
                                    variant="standard"
                                    type="text"
                                    label="Full name"
                                    {...getFieldProps('fullName')}
                                    error={Boolean(touched.fullName && errors.fullName)}
                                    helperText={touched.fullName && errors.fullName}
                                />
                            </Grid>
                            <Grid item xs={12} >
                                <TextFieldFirstStyle
                                    fullWidth
                                    variant="standard"
                                    type="text"
                                    label="Email address"
                                    {...getFieldProps('email')}
                                    error={Boolean(touched.email && errors.email)}
                                    helperText={touched.email && errors.email}
                                />
                            </Grid>
                            <Grid item xs={12} >
                                <TextFieldFirstStyle
                                    fullWidth
                                    variant="standard"
                                    type={showPassword ? 'text' : 'password'}
                                    label="Password"
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
                        </Grid>
                    </DialogContent>
                    <DialogActions sx={{ pt: "65px", pb: "26px", px: "32px" }}>
                        <LoadingButtonStyle
                            color="secondary"
                            variant="contained"
                            loading={isSubmitting}
                            type="submit"
                            sx={{ width: "100%", py: "15px" }}
                        >
                            Register
                        </LoadingButtonStyle>
                    </DialogActions>
                </Form>
            </FormikProvider>
        </DialogStyle>
    )
}

export default CreateShopDialog