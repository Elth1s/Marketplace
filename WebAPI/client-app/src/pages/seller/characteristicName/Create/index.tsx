import Grid from '@mui/material/Grid';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';

import * as Yup from 'yup';
import { FC, useState } from "react";
import { useTranslation } from 'react-i18next';
import { Form, FormikProvider, useFormik } from "formik";

import { AdminDialogButton } from '../../../../components/Button/style';
import TextFieldComponent from '../../../../components/TextField';
import AutocompleteComponent from '../../../../components/Autocomplete';
import DialogTitleWithButton from '../../../../components/Dialog/DialogTitleWithButton';
import IconButtonPlus from '../../../../components/Button/IconButtonPlus';

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from '../../../../hooks/useTypedSelector';

import { CreateProps, ServerError } from '../../../../store/types';

import { toLowerFirstLetter } from '../../../../http_comon';

import { ICharacteristicName } from "../types";
import { MenuItem } from '@mui/material';
import { TextFieldFirstStyle } from '../../../../components/TextField/styled';

const CharacteristicCreate: FC<CreateProps> = ({ afterCreate }) => {
    const { t } = useTranslation();

    const [open, setOpen] = useState(false);

    const { CreateCharacteristicName, GetCharacteristicGroups, GetUnits } = useActions();

    const { characteristicGroups } = useTypedSelector((store) => store.characteristicGroup);
    const { units } = useTypedSelector((store) => store.unit);

    const item: ICharacteristicName = {
        name: '',
        characteristicGroupId: 0,
        unitId: null,
    }

    const handleClickOpen = async () => {
        await GetCharacteristicGroups();
        await GetUnits();
        setOpen(true);
    };

    const handleClickClose = () => {
        setOpen(false);
        resetForm();
    };

    const validationFields = Yup.object().shape({
        name: Yup.string().min(2).max(30).required().label(t('validationProps.name')),
        characteristicGroupId: Yup.number().required().label(t('validationProps.characteristicGroup')),
        unitId: Yup.number().nullable().label(t('validationProps.unitMeasure')),
    });

    const onHandleSubmit = async (values: ICharacteristicName) => {
        try {
            await CreateCharacteristicName(values);
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
                    title={t('pages.seller.characteristicName.createTitle')}
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
                                {t('pages.seller.main.btnCreate')}
                            </AdminDialogButton>
                        </DialogActions>
                    </Form>
                </FormikProvider>
            </Dialog>
        </>
    )
}

export default CharacteristicCreate;