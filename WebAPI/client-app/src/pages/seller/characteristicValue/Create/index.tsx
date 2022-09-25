import Grid from '@mui/material/Grid';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';

import * as Yup from 'yup';
import { FC, useState } from "react";
import { useTranslation } from 'react-i18next';
import { Form, FormikProvider, useFormik } from "formik";

import { AdminDialogButton } from '../../../../components/Button/style';
import AutocompleteComponent from '../../../../components/Autocomplete';
import TextFieldComponent from '../../../../components/TextField';
import DialogTitleWithButton from '../../../../components/Dialog/DialogTitleWithButton';
import IconButtonPlus from '../../../../components/Button/IconButtonPlus';

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from '../../../../hooks/useTypedSelector';

import { CreateProps, ServerError } from '../../../../store/types';

import { toLowerFirstLetter } from '../../../../http_comon';

import { ICharacteristicValue } from "../types";
import { TextFieldFirstStyle } from '../../../../components/TextField/styled';
import { MenuItem } from '@mui/material';

const CharacteristicValueCreate: FC<CreateProps> = ({ afterCreate }) => {
    const { t } = useTranslation();

    const [open, setOpen] = useState(false);

    const { CreateCharacteristicValue, GetCharacteristicNames } = useActions();

    const { characteristicNames } = useTypedSelector((store) => store.characteristicName);

    const item: ICharacteristicValue = {
        value: '',
        characteristicNameId: 0,
    }

    const handleClickOpen = async () => {
        await GetCharacteristicNames();
        setOpen(true);
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
            await CreateCharacteristicValue(values);
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

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps, resetForm, setFieldValue } = formik;

    return (
        <>
            <IconButtonPlus onClick={handleClickOpen} />
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
                    title={t('pages.seller.characteristicValue.createTitle')}
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
                                        label={t('validationProps.characteristicName')}
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
                                {t('pages.seller.main.btnCreate')}
                            </AdminDialogButton>
                        </DialogActions>
                    </Form>
                </FormikProvider>
            </Dialog>
        </>
    )
}

export default CharacteristicValueCreate;