import { Box, DialogActions, DialogContent, DialogTitle, IconButton, Typography, Grid, Button, useTheme } from '@mui/material';
import { Close, StarRounded } from '@mui/icons-material';
import { LoadingButton } from '@mui/lab';

import * as Yup from 'yup';
import { FC, useState } from "react";
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';
import { toast } from 'react-toastify';
import { Form, FormikProvider, useFormik } from "formik";

import { IQuestion, IReview } from "../types";
import { ServerError } from '../../../../store/types';

import { ReviewQustionDialogStyle } from '../../../../components/Dialog/styled';
import { TextFieldSecondStyle } from '../../../../components/TextField/styled';
import { RatingStyle } from '../../../../components/Rating/styled';
import { ToastError, ToastWarning } from '../../../../components/ToastComponent';

import { useTypedSelector } from '../../../../hooks/useTypedSelector';
import { useActions } from "../../../../hooks/useActions";
import CropperDialog from '../../../../components/CropperDialog';


interface Props {
    getData: any
}


const QuestionForm: FC<Props> = ({ getData }) => {
    const { t } = useTranslation();
    const { palette } = useTheme();

    let { urlSlug } = useParams();

    const { AddQuestion } = useActions();
    const { user } = useTypedSelector(state => state.auth)

    const item: IQuestion = {
        fullName: "",
        email: user.isEmailExist ? user.emailOrPhone : "",
        message: "",
        images: [],
        productSlug: ""
    };

    const questionValidationFields = Yup.object().shape({
        fullName: Yup.string().required().min(2).max(80).label(t("validationProps.filterName")),
        email: Yup.string().required().label(t("validationProps.email")),
        message: Yup.string().required().min(2).max(500).label(t("validationProps.message")),
    });

    const onHandleSubmit = async (values: IQuestion) => {
        try {
            if (!urlSlug) {
                toast(<ToastError title={t("toastTitles.error")} message={t("components.questionDialog.exceptions.urlSlugError")} />);
                return;
            }

            values.productSlug = urlSlug;

            await AddQuestion(values);
            getData();
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
        enableReinitialize: true,
        validationSchema: questionValidationFields,
        onSubmit: onHandleSubmit
    });

    const onSaveImage = (base64: string) => {
        var tempImages = formik.values.images.slice();

        tempImages.push(base64);
        setFieldValue("images", tempImages);
    }

    const { errors, touched, isSubmitting, handleSubmit, setFieldValue, setFieldError, getFieldProps, resetForm } = formik;

    return (
        <FormikProvider value={formik} >
            <Form autoComplete="off" noValidate onSubmit={handleSubmit} >
                <Grid container rowSpacing="20px">
                    <Grid item xs={12}>
                        <TextFieldSecondStyle
                            fullWidth
                            variant="outlined"
                            type="text"
                            placeholder="Full name"
                            {...getFieldProps('fullName')}
                            error={Boolean(touched.fullName && errors.fullName)}
                            helperText={touched.fullName && errors.fullName}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextFieldSecondStyle
                            disabled={user.isEmailExist}
                            fullWidth
                            variant="outlined"
                            type="text"
                            placeholder="Email"
                            autoComplete="email"
                            {...getFieldProps('email')}
                            error={Boolean(touched.email && errors.email)}
                            helperText={touched.email && errors.email}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextFieldSecondStyle
                            fullWidth
                            multiline
                            rows={3}
                            variant="outlined"
                            type="text"
                            placeholder="Message"
                            {...getFieldProps('message')}
                            error={Boolean(touched.message && errors.message)}
                            helperText={touched.message && errors.message}
                        />
                    </Grid>
                    <Grid item xs={12} sx={{ display: "flex" }}>
                        {formik.values.images?.length != 0 &&
                            formik.values.images.map((row, index) => {
                                return (
                                    <img
                                        key={`product_image_${index}`}
                                        src={row}
                                        alt="icon"
                                        style={{ width: "100px", height: "100px", borderRadius: "10px", marginRight: "10px", marginBottom: "10px", border: "1px solid #F45626", objectFit: "contain" }} />
                                );
                            })
                        }
                        <CropperDialog imgSrc={""} onDialogSave={onSaveImage} />
                    </Grid>
                    <Grid item xs={12} sx={{ display: "flex", justifyContent: "end" }}>
                        <LoadingButton
                            color="primary"
                            variant="contained"
                            loading={isSubmitting}
                            type="submit"
                            sx={{ height: "55px", ml: "auto", fontSize: "20px", py: "15px", px: "71px", textTransform: "none" }}
                        >
                            {t("components.questionDialog.button")}
                        </LoadingButton>
                    </Grid>
                </Grid>
            </Form>
        </FormikProvider>
    )
}

export default QuestionForm;