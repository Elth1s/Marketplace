import Grid from '@mui/material/Grid';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';

import Edit from '@mui/icons-material/Edit';

import * as Yup from 'yup';
import { FC, useState } from "react";
import { useTranslation } from 'react-i18next';
import { Form, FormikProvider, useFormik } from "formik";

import { AdminDialogButton } from '../../../../components/Button/style';
import AutocompleteComponent from '../../../../components/Autocomplete';
import TextFieldComponent from '../../../../components/TextField';
import DialogTitleWithButton from '../../../../components/Dialog/DialogTitleWithButton';

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from '../../../../hooks/useTypedSelector';

import { UpdateProps, ServerError } from '../../../../store/types';

import { toLowerFirstLetter } from '../../../../http_comon';

import { ICharacteristicName } from '../types';
import { TextFieldFirstStyle } from '../../../../components/TextField/styled';
import { MenuItem } from '@mui/material';

const CharacteristicUpdate: FC<UpdateProps> = ({ id, afterUpdate }) => {
    const { t } = useTranslation();

    const [open, setOpen] = useState(false);

    const { GetByIdCharacteristicName, GetCharacteristicGroups, UpdateCharacteristicName } = useActions();

    const { selectedCharacteristicName } = useTypedSelector((store) => store.characteristicName);
    const { characteristicGroups } = useTypedSelector((store) => store.characteristicGroup);
    const { units } = useTypedSelector((store) => store.unit);

    const handleClickOpen = async () => {
        setOpen(true);
        await GetCharacteristicGroups();
        await GetByIdCharacteristicName(id);
    };

    const handleClickClose = () => {
        setOpen(false);
        resetForm();
    };

    const validationFields = Yup.object().shape({
        name: Yup.string().min(2).max(30).required().label(t('validationProps.name')),
        characteristicGroupId: Yup.number().required().label(t('validationProps.characteristicName')),
        unitId: Yup.number().nullable().label(t('validationProps.unitMeasure')),
    });

    const onHandleSubmit = async (values: ICharacteristicName) => {
        try {
            await UpdateCharacteristicName(id, values);
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
        initialValues: selectedCharacteristicName,
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
                sx={{
                    "& .MuiDialog-paper": {
                        maxWidth: "none",
                        width: "980px",
                        borderRadius: "10px",
                    }
                }}
            >
                <DialogTitleWithButton
                    title={t('pages.seller.characteristicName.updateTitle')}
                    onClick={handleClickClose}
                />
                <FormikProvider value={formik} >
                    <Form onSubmit={handleSubmit}>
                        <DialogContent sx={{ padding: "10px 40px 45px" }}>
                            <Grid container spacing={5.25}>
                                <Grid item xs={12}>
                                    <TextFieldComponent
                                        type="text"
                                        label={t('validationProps.name')}
                                        error={errors.name}
                                        touched={touched.name}
                                        getFieldProps={{ ...getFieldProps('name') }}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <TextFieldFirstStyle
                                        select
                                        fullWidth
                                        variant="standard"
                                        label={t('validationProps.characteristicGroup')}
                                        error={Boolean(touched.characteristicGroupId && errors.characteristicGroupId)}
                                        helperText={touched.characteristicGroupId && errors.characteristicGroupId}
                                        {...getFieldProps('characteristicGroupId')}
                                    >
                                        {characteristicGroups && characteristicGroups.map((item) =>
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
                                {t('pages.seller.main.btnСancel')}
                            </AdminDialogButton>
                            <AdminDialogButton
                                type="submit"
                                variant="contained"
                                color="primary"
                                disabled={isSubmitting}
                            >
                                {t('pages.seller.main.btnUpdate')}
                            </AdminDialogButton>
                        </DialogActions>
                    </Form>
                </FormikProvider>
            </Dialog>
        </>
    )
}

export default CharacteristicUpdate;