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
    TextField
} from '@mui/material';

import { TransitionProps } from '@mui/material/transitions';
import { Close, StarRounded } from '@mui/icons-material';
import { LoadingButton } from '@mui/lab';

import { forwardRef, useState } from 'react';
import { Form, FormikProvider, useFormik } from "formik";
import { useTranslation } from 'react-i18next';

import { ServerError } from '../../../../store/types';
import { RatingStyle } from '../../../../components/Rating/styled';

// import { validationFields } from '../validation';

const Transition = forwardRef(function Transition(
    props: TransitionProps & {
        children: React.ReactElement<any, any>;
    },
    ref: React.Ref<unknown>,
) {
    return <Slide direction="left" ref={ref} {...props} />;
});

interface IReview {
    fullName: string,
    email: string,
    qualityOfService: number,
    observanceOfTerms: number,
    informationRelevance: number,
    review: string,
}

const AddReview = () => {
    const { t } = useTranslation();
    const [open, setOpen] = useState(false);

    const item: IReview = {
        fullName: "",
        email: "",
        qualityOfService: 0,
        observanceOfTerms: 0,
        informationRelevance: 0,
        review: "",
    };

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClickClose = () => {
        setOpen(false);
        resetForm();
    };

    const onHandleSubmit = async (values: IReview) => {
        try {
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
    const formik = useFormik({
        initialValues: item,
        //  validationSchema: validationFields,
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
                    fontWeight: "600",
                    borderRadius: "10px",
                    padding: "14px 48px",
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
                <DialogTitle sx={{ p: "34px 28px" }}>
                    <Box sx={{
                        display: "flex",
                        justifyContent: "space-between",
                        alignItems: "flex-start"
                    }}>
                        <Typography sx={{ fontSize: "30px", lineHeight: "38px" }}>
                            {t('pages.seller.addReview.dialogTitle')}
                        </Typography>
                        <IconButton aria-label="close" onClick={handleClickClose}>
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
                                        <Typography variant="h5" sx={{ mb: "10px" }}>{t('pages.seller.addReview.service')}</Typography>
                                        <RatingStyle
                                            sx={{ fontSize: "30px" }}
                                            value={formik.values.qualityOfService}
                                            precision={1}
                                            icon={<StarRounded sx={{ fontSize: "30px" }} />}
                                            emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                                            onChange={(event, newValue: number | null) => {
                                                if (newValue != null)
                                                    setFieldValue("qualityOfService", newValue);
                                            }}

                                        />
                                    </Grid>
                                    <Grid item sx={{ textAlign: "center" }}>
                                        <Typography>{t('pages.seller.addReview.terms')}</Typography>
                                        <RatingStyle
                                            sx={{ fontSize: "30px" }}
                                            value={formik.values.observanceOfTerms}
                                            precision={0.5}
                                            icon={<StarRounded sx={{ fontSize: "30px" }} />}
                                            emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                                            onChange={(event, newValue: number | null) => {
                                                if (newValue != null)
                                                    setFieldValue("observanceOfTerms", newValue);
                                            }}

                                        />
                                    </Grid>
                                    <Grid item sx={{ textAlign: "center" }}>
                                        <Typography>{t('pages.seller.addReview.information')}</Typography>
                                        <RatingStyle
                                            sx={{ fontSize: "30px" }}
                                            value={formik.values.informationRelevance}
                                            precision={0.5}
                                            icon={<StarRounded sx={{ fontSize: "30px" }} />}
                                            emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                                            onChange={(event, newValue: number | null) => {
                                                if (newValue != null)
                                                    setFieldValue("informationRelevance", newValue);
                                            }}

                                        />
                                    </Grid>
                                </Grid>
                                <Grid item xs={12} sx={{ mb: "25px" }}>
                                    <TextField
                                        fullWidth
                                        variant="outlined"
                                        type="text"
                                        label={t('pages.seller.addReview.fullName')}
                                        error={Boolean(touched.fullName && errors.fullName)}
                                        helperText={touched.fullName && errors.fullName}
                                        {...getFieldProps("fullName")}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{ mb: "45px" }}>
                                    <TextField
                                        fullWidth
                                        variant="outlined"
                                        type="email"
                                        label={t('pages.seller.addReview.email')}
                                        error={Boolean(touched.email && errors.email)}
                                        helperText={touched.email && errors.email}
                                        {...getFieldProps("email")}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{ mb: "25px" }}>
                                    <TextField
                                        fullWidth
                                        multiline
                                        rows={5}
                                        variant="outlined"
                                        type="text"
                                        label={t('pages.seller.addReview.review')}
                                        error={Boolean(touched.review && errors.review)}
                                        helperText={touched.review && errors.review}
                                        {...getFieldProps("review")}
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

export default AddReview;