import Grid from '@mui/material/Grid';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import Slide from '@mui/material/Slide';

import Edit from '@mui/icons-material/Edit';

import { TransitionProps } from '@mui/material/transitions/transition';

import * as Yup from 'yup';
import { FC, forwardRef, useState } from "react";
import { useTranslation } from 'react-i18next';
import { Form, FormikProvider, useFormik } from "formik";

import { TextFieldFirstStyle } from '../../../../components/TextField/styled';
import { AdminDialogButton } from '../../../../components/Button/style';
import DialogTitleWithButton from '../../../../components/Dialog/DialogTitleWithButton';

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from '../../../../hooks/useTypedSelector';

import { UpdateProps, ServerError, } from '../../../../store/types';

import { toLowerFirstLetter } from '../../../../http_comon';

import { IFilterGroup } from "../types";
import AdminSellerDialog from '../../../../components/Dialog';
import { AdminSellerDialogActionsStyle, AdminSellerDialogContentStyle } from '../../../../components/Dialog/styled';

const FilterGroupUpdate: FC<UpdateProps> = ({ id, afterUpdate }) => {
    const { t } = useTranslation();

    const [open, setOpen] = useState(false);

    const { GetByIdFilterGroup, UpdateFilterGroup } = useActions();
    const { selectedFilterGroup } = useTypedSelector((store) => store.filterGroup);

    const handleClickOpen = async () => {
        setOpen(true);
        await GetByIdFilterGroup(id);
    };

    const handleClickClose = () => {
        setOpen(false);
        resetForm();
    };

    const onHandleSubmit = async (values: IFilterGroup) => {
        try {
            await UpdateFilterGroup(id, values);
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

    const validationFields = Yup.object().shape({
        englishName: Yup.string().min(2).max(30).required().label(t('validationProps.englishName')),
        ukrainianName: Yup.string().min(2).max(30).required().label(t(`validationProps.ukrainianName`))
    });

    const formik = useFormik({
        initialValues: selectedFilterGroup,
        validationSchema: validationFields,
        enableReinitialize: true,
        onSubmit: onHandleSubmit
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps, resetForm } = formik;

    return (
        <>
            <Edit onClick={() => handleClickOpen()} />
            <AdminSellerDialog
                open={open}
                onClose={handleClickClose}
                dialogContent={
                    <>
                        <DialogTitleWithButton
                            title={t('pages.admin.filterGroup.createTitle')}
                            onClick={handleClickClose}
                        />
                        <FormikProvider value={formik} >
                            <Form onSubmit={handleSubmit}>
                                <AdminSellerDialogContentStyle>
                                    <Grid container spacing={2}>
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
                                </AdminSellerDialogContentStyle>
                                <AdminSellerDialogActionsStyle>
                                    <AdminDialogButton
                                        type="submit"
                                        variant="contained"
                                        color="primary"
                                        disabled={isSubmitting}
                                    >
                                        {t('pages.admin.main.btnCreate')}
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

export default FilterGroupUpdate;