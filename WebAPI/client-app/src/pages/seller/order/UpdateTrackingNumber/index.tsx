import Grid from "@mui/material/Grid";
import { Edit } from "@mui/icons-material";

import { FC, useState } from "react";
import { useParams } from "react-router-dom";
import { useTranslation } from "react-i18next";

import { Form, FormikProvider, useFormik } from "formik";
import * as Yup from 'yup';

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { ServerError, UpdateProps } from "../../../../store/types";

import AutocompleteComponent from "../../../../components/Autocomplete";
import { TextFieldFirstStyle } from "../../../../components/TextField/styled";

import { toLowerFirstLetter } from "../../../../http_comon";
import AdminSellerDialog from "../../../../components/Dialog";
import DialogTitleWithButton from "../../../../components/Dialog/DialogTitleWithButton";
import { AdminSellerDialogActionsStyle, AdminSellerDialogContentStyle } from "../../../../components/Dialog/styled";
import { AdminDialogButton } from "../../../../components/Button/style";
import { Button, Typography } from "@mui/material";
import CropperDialog from "../../../../components/CropperDialog";
import { IOrderUpdate } from "../type";
import { orderStatusIdCompleted, orderStatusIdSent } from "../constants";


const UpdateTrackingNumber: FC<UpdateProps> = ({ id, afterUpdate }) => {
    const { t } = useTranslation();

    const { UpdateOrderForSeller } = useActions();

    const [open, setOpen] = useState(false);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClickClose = () => {
        setOpen(false);
        resetForm();
    };

    const validationFields = Yup.object().shape({
        trackingNumber: Yup.string().required().label(t('validationProps.trackingNumber')),
    });

    const item: IOrderUpdate = {
        trackingNumber: "",
        orderStatusId: orderStatusIdSent
    }

    const formik = useFormik({
        initialValues: item,
        validationSchema: validationFields,
        enableReinitialize: true,
        onSubmit: async (values, { setFieldError }) => {
            try {
                await UpdateOrderForSeller(id, values)
                afterUpdate();
                handleClickClose();
            }
            catch (ex) {
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
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldValue, getFieldProps, resetForm } = formik;

    return (
        <>
            <Button
                color="secondary"
                variant="outlined"
                sx={{
                    width: "auto",
                    px: "15.5px",
                    py: "8.5px",
                    textTransform: "none",
                    borderRadius: "10px",
                    fontSize: "18px",
                    height: "40px",
                    border: "1px solid #0E7C3A",
                    mt: "10px"
                }}
                onClick={() => handleClickOpen()}
            >
                {t("pages.user.order.addTrackingNumber")}
            </Button>
            <AdminSellerDialog
                open={open}
                onClose={handleClickClose}
                dialogContent={
                    <>
                        <DialogTitleWithButton
                            title={t("pages.user.order.addTrackingNumber")}
                            onClick={handleClickClose}
                        />
                        <FormikProvider value={formik} >
                            <Form onSubmit={handleSubmit}>
                                <AdminSellerDialogContentStyle>
                                    <Grid container spacing={2}>
                                        <Grid item xs={12}>
                                            <TextFieldFirstStyle
                                                fullWidth
                                                variant="standard"
                                                type="text"
                                                label={t('validationProps.trackingNumber')}
                                                {...getFieldProps('trackingNumber')}
                                                error={Boolean(touched.trackingNumber && errors.trackingNumber)}
                                                helperText={touched.trackingNumber && errors.trackingNumber}
                                            />
                                        </Grid>
                                    </Grid>
                                </AdminSellerDialogContentStyle>
                                <AdminSellerDialogActionsStyle>
                                    <AdminDialogButton
                                        type="submit"
                                        variant="contained"
                                        color="primary"
                                        disabled={isSubmitting}
                                    >
                                        {t('pages.admin.main.btnUpdate')}
                                    </AdminDialogButton>
                                </AdminSellerDialogActionsStyle>
                            </Form>
                        </FormikProvider>
                    </>
                }
            />
        </>
    )
}

export default UpdateTrackingNumber;