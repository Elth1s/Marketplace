import { Close } from '@mui/icons-material'
import { Dialog, DialogActions, DialogContent, DialogTitle, Grid, IconButton, InputAdornment, Typography } from '@mui/material'
import { Form, FormikProvider, useFormik } from 'formik'
import { FC, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { black_eye, eye_off } from '../../../assets/icons'
import { DialogStyle } from '../../../components/Dialog/styled'
import { LoadingButtonStyle } from '../../../components/LoadingButton/styled'
import { TextFieldFirstStyle } from '../../../components/TextField/styled'
import { useActions } from '../../../hooks/useActions'
import { toLowerFirstLetter } from '../../../http_comon'
import { ServerError } from '../../../store/types'
import { ICreateShop } from './types'
import * as Yup from 'yup';
import { useTranslation } from 'react-i18next'

interface Props {
    dialogOpen: boolean,
    dialogClose: any
}

const CreateShopDialog: FC<Props> = ({ dialogOpen, dialogClose }) => {
    const { t } = useTranslation();
    const { RegisterShop } = useActions();

    const navigate = useNavigate();

    const [showPassword, setShowPassword] = useState(false);
    const shopModel: ICreateShop = { name: '', siteUrl: '', fullName: '', email: '', password: '' };

    const CreateShopSchema = Yup.object().shape({
    //     firstName: Yup.string().min(3).max(50).required().label(t('validationProps.firstName')),
    //     secondName: Yup.string().min(3).max(75).required().label(t('validationProps.lastName')),
    //     emailOrPhone: Yup.string().required().label(t('validationProps.emailOrPhone')),
    });

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
                <Typography color="inherit" fontSize="30px" align="center" lineHeight="38px">
                    {t('pages.seller.createShop.title')}
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
                        <Typography variant="h4" color="inherit" lineHeight="23px">
                            {t('pages.seller.createShop.store')}
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
                                    label={t('validationProps.name')}
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
                                    label={t('validationProps.siteURL')}
                                    {...getFieldProps('siteUrl')}
                                    error={Boolean(touched.siteUrl && errors.siteUrl)}
                                    helperText={touched.siteUrl && errors.siteUrl}
                                />
                            </Grid>
                        </Grid>
                        <Typography variant="h4" color="inherit" lineHeight="23px">
                            {t('pages.seller.createShop.seller')}
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
                                    label={t('validationProps.fullName')}
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
                                    label={t('validationProps.email')}
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
                        </Grid>
                    </DialogContent>
                    <DialogActions sx={{ pt: "65px", pb: "26px", px: "32px" }}>
                        <LoadingButtonStyle
                            fullWidth
                            color="secondary"
                            variant="contained"
                            loading={isSubmitting}
                            type="submit"
                            sx={{ py: "15px", textTransform: "none" }}
                        >
                            {t('pages.seller.createShop.btn')}
                        </LoadingButtonStyle>
                    </DialogActions>
                </Form>
            </FormikProvider>
        </DialogStyle>
    )
}

export default CreateShopDialog