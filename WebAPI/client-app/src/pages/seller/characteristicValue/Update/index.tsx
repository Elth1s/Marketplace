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

import { ICharacteristicValue } from '../types';
import { TextFieldFirstStyle } from '../../../../components/TextField/styled';
import { MenuItem } from '@mui/material';

const CharacteristicValueUpdate: FC<UpdateProps> = ({ id, afterUpdate }) => {
    const { t } = useTranslation();

    const [open, setOpen] = useState(false);

    const { GetByIdCharacteristicValue, GetCharacteristicNames, UpdateCharacteristicValue } = useActions();

    const { selectedCharacteristicValue } = useTypedSelector((store) => store.characteristicValue);
    const { characteristicNames } = useTypedSelector((store) => store.characteristicName);

    const handleClickOpen = async () => {
        setOpen(true);
        await GetCharacteristicNames();
        await GetByIdCharacteristicValue(id);
    };

    const handleClickClose = () => {
        setOpen(false);
        resetForm();
    };

    const validationFields = Yup.object().shape({
        value: Yup.string().min(1).max(30).required().label(t('validationProps.value')),
        characteristicNameId: Yup.number().required().label(t('validationProps.value')),
    });

    const onHandleSubmit = async (values: ICharacteristicValue) => {
        try {
            await UpdateCharacteristicValue(id, values);
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
        initialValues: selectedCharacteristicValue,
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
                    title={t('pages.seller.characteristicGroup.updateTitle')}
                    onClick={handleClickClose}
                />
                <FormikProvider value={formik} >
                    <Form onSubmit={handleSubmit}>
                        <DialogContent sx={{ padding: "10px 40px 45px" }}>
                            <Grid container spacing={5.25}>
                                <Grid item xs={12}>
                                    <TextFieldComponent
                                        type="text"
                                        label={t('validationProps.value')}
                                        error={errors.value}
                                        touched={touched.value}
                                        getFieldProps={{ ...getFieldProps('value') }}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <TextFieldFirstStyle
                                        select
                                        fullWidth
                                        variant="standard"
                                        label={t("validationProps.characteristicName")}
                                        error={Boolean(touched.characteristicNameId && errors.characteristicNameId)}
                                        helperText={touched.characteristicNameId && errors.characteristicNameId}
                                        {...getFieldProps('characteristicNameId')}
                                    >
                                        {characteristicNames && characteristicNames.map((item) =>
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

export default CharacteristicValueUpdate;