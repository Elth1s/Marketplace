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
import { useTypedSelector } from '../../../../hooks/useTypedSelector';

import { CreateProps, ServerError } from '../../../../store/types';

import { toLowerFirstLetter } from '../../../../http_comon';

import { IFilterValue } from "../types";
import AutocompleteComponent from '../../../../components/Autocomplete';

const Transition = forwardRef(function Transition(
    props: TransitionProps & {
        children: React.ReactElement<any, any>;
    },
    ref: React.Ref<unknown>,
) {
    return <Slide direction="left" ref={ref} {...props} />;
});

const FilterValueCreate: FC<CreateProps> = ({ afterCreate }) => {
    const { t } = useTranslation();

    const [open, setOpen] = useState(false);

    const { CreateFilterValue, GetFilterNames } = useActions();

    const { filterNames } = useTypedSelector((store) => store.filterName);

    const item: IFilterValue = {
        englishValue: "",
        ukrainianValue: "",
        min: "",
        max: "",
        filterNameId: 0,
    }

    const handleClickOpen = async () => {
        await GetFilterNames();
        setOpen(true);
    };

    const handleClickClose = () => {
        setOpen(false);
        resetForm();
    };

    const validationFields = Yup.object().shape({
        englishValue: Yup.string().min(2).max(30).required().label(t('validationProps.englishValue')),
        ukrainianValue: Yup.string().min(2).max(30).required().label(t('validationProps.englishValue')),
        filterNameId: Yup.number().required().label(t('validationProps.filterName')),
        min: Yup.number().nullable().label(t('validationProps.min')),
        max: Yup.number().nullable().label(t('validationProps.max')),
    });

    const onHandleSubmit = async (values: IFilterValue) => {
        try {
            await CreateFilterValue(values);
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
        validationSchema: validationFields,
        onSubmit: onHandleSubmit
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldValue, setFieldError, getFieldProps, resetForm } = formik;

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
                    title={t('pages.admin.filterValue.createTitle')}
                    onClick={handleClickClose}
                />
                <FormikProvider value={formik} >
                    <Form onSubmit={handleSubmit}>
                        <DialogContent sx={{ padding: "10px 40px 45px" }}>
                            <Grid container spacing={5.25}>
                                <Grid item xs={12}>
                                    <TextFieldFirstStyle
                                        fullWidth
                                        autoComplete="englishValue"
                                        variant="standard"
                                        type="text"
                                        label={t('validationProps.englishValue')}
                                        {...getFieldProps('englishValue')}
                                        error={Boolean(touched.englishValue && errors.englishValue)}
                                        helperText={touched.englishValue && errors.englishValue}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <TextFieldFirstStyle
                                        fullWidth
                                        autoComplete="ukrainianValue"
                                        variant="standard"
                                        type="text"
                                        label={t('validationProps.englishValue')}
                                        {...getFieldProps('ukrainianValue')}
                                        error={Boolean(touched.ukrainianValue && errors.ukrainianValue)}
                                        helperText={touched.ukrainianValue && errors.ukrainianValue}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <TextFieldFirstStyle
                                        fullWidth
                                        variant="standard"
                                        autoComplete="min"
                                        type="text"
                                        label={t('validationProps.min')}
                                        {...getFieldProps('min')}
                                        error={Boolean(touched.min && errors.min)}
                                        helperText={touched.min && errors.min}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <TextFieldFirstStyle
                                        fullWidth
                                        variant="standard"
                                        autoComplete="max"
                                        type="text"
                                        label={t('validationProps.max')}
                                        {...getFieldProps('max')}
                                        error={Boolean(touched.max && errors.max)}
                                        helperText={touched.max && errors.max}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <AutocompleteComponent
                                        label={t('validationProps.filterName')}
                                        name="filterNameId"
                                        error={errors.filterNameId}
                                        touched={touched.filterNameId}
                                        options={filterNames}
                                        getOptionLabel={(option) => option.name}
                                        isOptionEqualToValue={(option, value) => option?.id === value.id}
                                        defaultValue={undefined}
                                        onChange={(e, value) => { setFieldValue("filterNameId", value?.id) }}
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

export default FilterValueCreate;