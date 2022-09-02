import Grid from '@mui/material/Grid';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import Slide from '@mui/material/Slide';

import { TransitionProps } from '@mui/material/transitions/transition';

import * as Yup from 'yup';
import { FC, forwardRef, useState } from "react";
import { useTranslation } from 'react-i18next';
import { Form, FormikProvider, useFormik } from "formik";

import { TextFieldFirstStyle } from '../../../../components/TextField/styled';
import { AdminDialogButton } from '../../../../components/Button/style';
import DialogTitleWithButton from '../../../../components/Dialog/DialogTitleWithButton';
import IconButtonPlus from '../../../../components/Button/IconButtonPlus';

import { useActions } from "../../../../hooks/useActions";

import { CreateProps, ServerError } from '../../../../store/types';

import { toLowerFirstLetter } from '../../../../http_comon';

import { IProductStatus } from "../types";

const Transition = forwardRef(function Transition(
    props: TransitionProps & {
        children: React.ReactElement<any, any>;
    },
    ref: React.Ref<unknown>,
) {
    return <Slide direction="left" ref={ref} {...props} />;
});

const ProductStatusCreate: FC<CreateProps> = ({ afterCreate }) => {
    const { t } = useTranslation();

    const [open, setOpen] = useState(false);

    const { CreateProductStatus } = useActions();

    const item: IProductStatus = {
        englishName: "",
        ukrainianName: "",
    };

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClickClose = () => {
        setOpen(false);
        resetForm();
    };

    const fieldValidation = Yup.object().shape({
        englishName: Yup.string().min(2).max(20).required().label(t('validationProps.englishName')),
        ukrainianName: Yup.string().min(2).max(20).required().label(t('validationProps.englishName')),
    });

    const onHandleSubmit = async (values: IProductStatus) => {
        try {
            await CreateProductStatus(values);
            afterCreate();
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
                        setFieldError(toLowerFirstLetter(key), message);
                    }
                });
        }
    }

    const formik = useFormik({
        initialValues: item,
        validationSchema: fieldValidation,
        onSubmit: onHandleSubmit
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps, resetForm } = formik;

    return (
        <>
            <IconButtonPlus onClick={handleClickOpen} />

            <Dialog
                open={open}
                TransitionComponent={Transition}
                sx={{
                    "& .MuiDialog-paper": {
                        maxWidth: "none",
                        width: "980px",
                        borderRadius: "10px",
                    }
                }}
            >
                <DialogTitleWithButton
                    title={t('pages.admin.productStatus.createTitle')}
                    onClick={handleClickClose}
                />
                <FormikProvider value={formik} >
                    <Form onSubmit={handleSubmit}>
                        <DialogContent sx={{ padding: "10px 40px 45px" }}>
                            <Grid container rowSpacing={5.25}>
                                <Grid item xs={12}>
                                    <TextFieldFirstStyle
                                        fullWidth
                                        autoComplete="englishName"
                                        variant="standard"
                                        type="text"
                                        label={t('validationProps.englishName')}
                                        {...getFieldProps('englishName')}
                                        error={Boolean(touched.englishName && errors.englishName)}
                                        helperText={touched.englishName && errors.englishName}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <TextFieldFirstStyle
                                        fullWidth
                                        autoComplete="ukrainianName"
                                        variant="standard"
                                        type="text"
                                        label={t('validationProps.ukrainianName')}
                                        {...getFieldProps('ukrainianName')}
                                        error={Boolean(touched.ukrainianName && errors.ukrainianName)}
                                        helperText={touched.ukrainianName && errors.ukrainianName}
                                    />
                                </Grid>
                            </Grid>
                        </DialogContent>
                        <DialogActions sx={{ padding: "0 40px 45px" }}>
                            <AdminDialogButton
                                type="submit"
                                variant="outlined"
                                color="primary"
                                onClick={handleClickClose}
                            >
                                {t('pages.admin.main.btnСancel')}
                            </AdminDialogButton>
                            <AdminDialogButton
                                type="submit"
                                variant="contained"
                                color="primary"
                                disabled={isSubmitting}
                            >
                                {t('pages.admin.main.btnCreate')}
                            </AdminDialogButton>
                        </DialogActions>
                    </Form>
                </FormikProvider>
            </Dialog>
        </>
    )
}

export default ProductStatusCreate;