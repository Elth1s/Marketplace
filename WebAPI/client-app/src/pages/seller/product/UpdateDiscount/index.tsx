import Grid from "@mui/material/Grid";
import { Percent } from "@mui/icons-material";

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
import { IProductDiscountRequest } from "../types";

interface UpdateDiscountProps {
    id: number,
    discount: number,
    saleId: number,
    afterUpdate: any
}

const UpdateProductDiscount: FC<UpdateDiscountProps> = ({ id, discount, saleId, afterUpdate }) => {
    const { t } = useTranslation();

    const { UpdateDiscountForSeller } = useActions();

    const [open, setOpen] = useState(false);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClickClose = () => {
        setOpen(false);
        resetForm();
    };

    const validationFields = Yup.object().shape({
        discount: Yup.number().required().label(t('validationProps.discount')),
    });

    const item: IProductDiscountRequest = {
        discount: discount,
        saleId: saleId
    }

    const formik = useFormik({
        initialValues: item,
        validationSchema: validationFields,
        enableReinitialize: true,
        onSubmit: async (values, { setFieldError }) => {
            try {
                await UpdateDiscountForSeller(id, values)
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
            <Percent onClick={() => handleClickOpen()} />
            <AdminSellerDialog
                open={open}
                onClose={handleClickClose}
                dialogContent={
                    <>
                        <DialogTitleWithButton
                            title={t("pages.seller.product.updateDiscount")}
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
                                                label={t('validationProps.discount')}
                                                {...getFieldProps('discount')}
                                                error={Boolean(touched.discount && errors.discount)}
                                                helperText={touched.discount && errors.discount}
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

export default UpdateProductDiscount;