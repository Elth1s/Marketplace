import {
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    Grid,
    Box,
    Slide,
    Typography,
    IconButton,
    Button,
    TextField,
    useTheme
} from '@mui/material';

import { TransitionProps } from '@mui/material/transitions';
import { Close, StarRounded } from '@mui/icons-material';
import { LoadingButton } from '@mui/lab';

import { FC, forwardRef, useState } from 'react';
import { Form, FormikProvider, useFormik } from "formik";
import { useTranslation } from 'react-i18next';

import { ServerError } from '../../../../store/types';
import { RatingStyle } from '../../../../components/Rating/styled';
import * as Yup from 'yup';
import { IShopReview } from '../types';
import { useParams } from 'react-router-dom';
import { useActions } from '../../../../hooks/useActions';
import { useTypedSelector } from '../../../../hooks/useTypedSelector';
import { ToastError, ToastWarning } from '../../../../components/ToastComponent';
import { toast } from 'react-toastify';
import { TextFieldSecondStyle } from '../../../../components/TextField/styled';

const Transition = forwardRef(function Transition(
    props: TransitionProps & {
        children: React.ReactElement<any, any>;
    },
    ref: React.Ref<unknown>,
) {
    return <Slide direction="left" ref={ref} {...props} />;
});

interface Props {
    getData: any
}

const AddShopReview: FC<Props> = ({ getData }) => {
    const { t } = useTranslation();
    const { palette } = useTheme();

    const [open, setOpen] = useState(false);

    const { AddShopReview } = useActions();
    const { user } = useTypedSelector(state => state.auth)

    let { shopId } = useParams();

    const item: IShopReview = {
        fullName: "",
        email: user.isEmailExist ? user.emailOrPhone : "",
        serviceQualityRating: 0,
        timelinessRating: 0,
        informationRelevanceRating: 0,
        comment: "",
        shopId: 0
    };

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClickClose = () => {
        setOpen(false);
        resetForm();
    };

    const onHandleSubmit = async (values: IShopReview) => {
        try {
            if (!shopId) {
                toast(<ToastError title={t("toastTitles.error")} message={t("pages.seller.addReview.exceptions.shopIdError")} />);
                return;
            }

            if (values.timelinessRating == 0 || values.serviceQualityRating == 0 || values.informationRelevanceRating == 0) {
                toast(<ToastWarning title={t("toastTitles.warning")} message={t("pages.seller.addReview.exceptions.RatingWarning")} />);
                return;
            }
            values.shopId = +shopId;

            await AddShopReview(values);
            getData();
            handleClickClose();
        } catch (ex) {
            const serverErrors = ex as ServerError;
            if (serverErrors.errors)
                Object.entries(serverErrors.errors).forEach(([key, value]) => {
                    if (Array.isArray(value)) {
                        let message = "";
                        value.forEach((item) => {
                            message += `${item} `;
                        });
                        setFieldError(key.toLowerCase(), message);
                    }
                });
        }
    }
    const shopReviewValidationFields = Yup.object().shape({
        fullName: Yup.string().required().min(2).max(60).label(t('validationProps.fullName')),
        email: Yup.string().required().label(t('validationProps.email')),
        comment: Yup.string().max(450).label(t('validationProps.comment')),
    });

    const formik = useFormik({
        initialValues: item,
        validationSchema: shopReviewValidationFields,
        onSubmit: onHandleSubmit
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, setFieldValue, getFieldProps, resetForm } = formik;

    return (
        <>
            <Button
                variant="contained"
                onClick={handleClickOpen}
                sx={{
                    fontSize: "24px",
                    lineHeight: "30px",
                    fontWeight: "500",
                    borderRadius: "10px",
                    padding: "14px 48px",
                    textTransform: "none",
                    "&:hover": {
                        background: palette.primary.main
                    },
                    "&& .MuiTouchRipple-child": {
                        backgroundColor: palette.primary.main
                    }
                }}
            >
                {t('pages.seller.addReview.button')}
            </Button>
            <Dialog
                open={open}
                maxWidth="sm"
                fullWidth={true}
                onClose={handleClickClose}
                TransitionComponent={Transition}
                PaperProps={{
                    sx: { minWidth: { sm: "925px" } },
                    style: { borderRadius: 12 }
                }}>
                <DialogTitle color="inherit" sx={{ p: "34px 28px" }}>
                    <Box sx={{
                        display: "flex",
                        justifyContent: "space-between",
                        alignItems: "flex-start"
                    }}>
                        <Typography color="inherit" sx={{ fontSize: "30px", lineHeight: "38px" }}>
                            {t('pages.seller.addReview.dialogTitle')}
                        </Typography>
                        <IconButton aria-label="close" color="inherit" onClick={handleClickClose}>
                            <Close />
                        </IconButton>
                    </Box>
                </DialogTitle>
                <FormikProvider value={formik} >
                    <Form onSubmit={handleSubmit}>
                        <DialogContent sx={{ p: "34px 28px" }}>
                            <Grid container>
                                <Grid item container xs={12} sx={{ justifyContent: "space-between", textAlig: "center", mb: "45px" }}>
                                    <Grid item sx={{ textAlign: "center" }}>
                                        <Typography variant="h5" color="inherit" sx={{ mb: "10px" }}>{t('pages.seller.addReview.service')}</Typography>
                                        <RatingStyle
                                            sx={{ fontSize: "30px" }}
                                            value={formik.values.serviceQualityRating}
                                            precision={1}
                                            icon={<StarRounded sx={{ fontSize: "30px" }} />}
                                            emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                                            onChange={(event, newValue: number | null) => {
                                                if (newValue != null)
                                                    setFieldValue("serviceQualityRating", newValue);
                                            }}
                                        />
                                    </Grid>
                                    <Grid item sx={{ textAlign: "center" }}>
                                        <Typography color="inherit">{t('pages.seller.addReview.terms')}</Typography>
                                        <RatingStyle
                                            sx={{ fontSize: "30px" }}
                                            value={formik.values.timelinessRating}
                                            precision={1}
                                            icon={<StarRounded sx={{ fontSize: "30px" }} />}
                                            emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                                            onChange={(event, newValue: number | null) => {
                                                if (newValue != null)
                                                    setFieldValue("timelinessRating", newValue);
                                            }}
                                        />
                                    </Grid>
                                    <Grid item sx={{ textAlign: "center" }}>
                                        <Typography color="inherit">{t('pages.seller.addReview.information')}</Typography>
                                        <RatingStyle
                                            sx={{ fontSize: "30px" }}
                                            value={formik.values.informationRelevanceRating}
                                            precision={1}
                                            icon={<StarRounded sx={{ fontSize: "30px" }} />}
                                            emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                                            onChange={(event, newValue: number | null) => {
                                                if (newValue != null)
                                                    setFieldValue("informationRelevanceRating", newValue);
                                            }}
                                        />
                                    </Grid>
                                </Grid>
                                <Grid item xs={12} sx={{ mb: "25px" }}>
                                    <TextFieldSecondStyle
                                        fullWidth
                                        variant="outlined"
                                        type="text"
                                        placeholder={t('pages.seller.addReview.fullName')}
                                        error={Boolean(touched.fullName && errors.fullName)}
                                        helperText={touched.fullName && errors.fullName}
                                        {...getFieldProps("fullName")}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{ mb: "45px" }}>
                                    <TextFieldSecondStyle
                                        disabled={user.isEmailExist}
                                        fullWidth
                                        variant="outlined"
                                        type="email"
                                        placeholder={t('pages.seller.addReview.email')}
                                        error={Boolean(touched.email && errors.email)}
                                        helperText={touched.email && errors.email}
                                        {...getFieldProps("email")}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{ mb: "25px" }}>
                                    <TextFieldSecondStyle
                                        fullWidth
                                        multiline
                                        rows={5}
                                        variant="outlined"
                                        type="text"
                                        placeholder={t('pages.seller.addReview.comment')}
                                        error={Boolean(touched.comment && errors.comment)}
                                        helperText={touched.comment && errors.comment}
                                        {...getFieldProps("comment")}
                                    />
                                </Grid>
                            </Grid>
                        </DialogContent>
                        <DialogActions sx={{ m: 0, p: 3, pt: 0 }}>
                            <LoadingButton
                                fullWidth
                                type="submit"
                                color="secondary"
                                variant="contained"
                                loading={isSubmitting}
                                sx={{
                                    fontSize: "20px",
                                    lineHeight: "25px",
                                    py: "15px",
                                }}
                            >
                                {t('pages.seller.addReview.dialogActions')}
                            </LoadingButton>
                        </DialogActions>
                    </Form>
                </FormikProvider>
            </Dialog>
        </>
    )
}

export default AddShopReview;