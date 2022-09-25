import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';
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

import { IFilterName } from "../types";
import { MenuItem } from '@mui/material';

const Transition = forwardRef(function Transition(
    props: TransitionProps & {
        children: React.ReactElement<any, any>;
    },
    ref: React.Ref<unknown>,
) {
    return <Slide direction="left" ref={ref} {...props} />;
});

const FilterUpdate: FC<UpdateProps> = ({ id, afterUpdate }) => {
    const { t } = useTranslation();

    const [open, setOpen] = useState(false);

    const { GetByIdFilterName, GetFilterGroups, UpdateFilterName } = useActions();

    const { selectedFilterName } = useTypedSelector((store) => store.filterName);
    const { filterGroups } = useTypedSelector((store) => store.filterGroup);
    const { units } = useTypedSelector((store) => store.unit);

    const handleClickOpen = async () => {
        setOpen(true);
        await GetFilterGroups();
        await GetByIdFilterName(id);
    };

    const handleClickClose = () => {
        setOpen(false);
        resetForm();
    };

    const validationFields = Yup.object().shape({
        englishName: Yup.string().min(2).max(30).required().label(t('validationProps.englishName')),
        ukrainianName: Yup.string().min(2).max(30).required().label(t('validationProps.ukrainianName')),
        filterGroupId: Yup.number().required().label(t('validationProps.filterGroup')),
        unitId: Yup.number().nullable().label(t('validationProps.unitMeasure')),
    });

    const onHandleSubmit = async (values: IFilterName) => {
        try {
            await UpdateFilterName(id, values);
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
        initialValues: selectedFilterName,
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
                    title={t('pages.admin.filterName.updateTitle')}
                    onClick={handleClickClose}
                />
                <FormikProvider value={formik} >
                    <Form onSubmit={handleSubmit}>
                        <DialogContent sx={{ padding: "10px 40px 45px" }}>
                            <Grid container spacing={5.25}>
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
                                <Grid item xs={12}>
                                    <TextFieldFirstStyle
                                        select
                                        fullWidth
                                        variant="standard"
                                        label={t('validationProps.filterGroup')}
                                        error={Boolean(touched.filterGroupId && errors.filterGroupId)}
                                        helperText={touched.filterGroupId && errors.filterGroupId}
                                        {...getFieldProps('filterGroupId')}
                                    >
                                        {filterGroups && filterGroups.map((item) =>
                                            <MenuItem key={item.id} value={item.id}>{item.name}</MenuItem>
                                        )}
                                    </TextFieldFirstStyle>

                                </Grid>
                                <Grid item xs={12}>
                                    <TextFieldFirstStyle
                                        select
                                        fullWidth
                                        variant="standard"
                                        label={t('validationProps.unitMeasure')}
                                        error={Boolean(touched.unitId && errors.unitId)}
                                        helperText={touched.unitId && errors.unitId}
                                        {...getFieldProps('unitId')}
                                    >
                                        {units && units.map((item) =>
                                            <MenuItem key={item.id} value={item.id}>{item.measure}</MenuItem>
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

export default FilterUpdate;