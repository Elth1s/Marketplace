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
import AutocompleteComponent from '../../../../components/Autocomplete';

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from '../../../../hooks/useTypedSelector';

import { UpdateProps, ServerError, } from '../../../../store/types';

import { toLowerFirstLetter } from '../../../../http_comon';

import { IFilterValue } from "../types";
import { MenuItem } from '@mui/material';

const Transition = forwardRef(function Transition(
    props: TransitionProps & {
        children: React.ReactElement<any, any>;
    },
    ref: React.Ref<unknown>,
) {
    return <Slide direction="left" ref={ref} {...props} />;
});

const FilterValueUpdate: FC<UpdateProps> = ({ id, afterUpdate }) => {
    const { t } = useTranslation();

    const [open, setOpen] = useState(false);

    const { GetByIdFilterValue, GetFilterNames, UpdateFilterValue } = useActions();

    const { selectedFilterValue } = useTypedSelector((store) => store.filterValue);
    const { filterNames } = useTypedSelector((store) => store.filterName);

    const handleClickOpen = async () => {
        setOpen(true);
        await GetFilterNames();
        await GetByIdFilterValue(id);
    };

    const handleClickClose = () => {
        setOpen(false);
        resetForm();
    };

    const validationFields = Yup.object().shape({
        englishValue: Yup.string().min(2).max(30).required().label(t('validationProps.englishValue')),
        ukrainianValue: Yup.string().min(2).max(30).required().label(t('validationProps.ukrainianValue')),
        countryId: Yup.number().required().label(t('validationProps.country'))
    });

    const onHandleSubmit = async (values: IFilterValue) => {
        try {
            await UpdateFilterValue(id, values);
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

    const formik = useFormik({
        initialValues: selectedFilterValue,
        validationSchema: validationFields,
        enableReinitialize: true,
        onSubmit: onHandleSubmit
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps, setFieldValue, resetForm } = formik;

    return (
        <>
            <Edit onClick={() => handleClickOpen()} />

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
                    title={t('pages.admin.filterValue.updateTitle')}
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
                                        label={t('validationProps.ukrainianValue')}
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
                                        label={t('validationProps.ukrainianValue')}
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
                                    <TextFieldFirstStyle
                                        select
                                        fullWidth
                                        variant="standard"
                                        label={t('validationProps.filterName')}
                                        error={Boolean(touched.filterNameId && errors.filterNameId)}
                                        helperText={touched.filterNameId && errors.filterNameId}
                                        {...getFieldProps('filterNameId')}
                                    >
                                        {filterNames && filterNames.map((item) =>
                                            <MenuItem key={item.id} value={item.id}>{item.name}</MenuItem>
                                        )}
                                    </TextFieldFirstStyle>

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
                                {t('pages.admin.main.btnUpdate')}
                            </AdminDialogButton>
                        </DialogActions>
                    </Form>
                </FormikProvider>
            </Dialog>
        </>
    )
}

export default FilterValueUpdate;