import Grid from '@mui/material/Grid';
import IconButton from '@mui/material/IconButton';
import EditIcon from '@mui/icons-material/Edit';

import { FC, useState } from "react";
import { useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { validationFields } from "../validation";
import { CharacteristicServerError, ICharacteristic } from "../types";

import { ICharacteristicUpdate } from './type';

import DialogComponent from '../../../../components/Dialog';
import SelectComponent from '../../../../components/Select';
import TextFieldComponent from '../../../../components/TextField';

const CharacteristicUpdate: FC<ICharacteristicUpdate> = ({ id }) => {
    const [open, setOpen] = useState(false);

    const { GetByIdCharacteristic, GetCharacteristicGroups, UpdateCharacteristic, GetCharacteristics } = useActions();

    const { characteristicInfo } = useTypedSelector((store) => store.characteristic);
    const { characteristicGroups } = useTypedSelector((store) => store.characteristicGroup);

    const item: ICharacteristic = {
        name: characteristicInfo.name,
        characteristicGroupId: characteristicGroups.find(n => n.name === characteristicInfo.characteristicGroupName)?.id || ''
    }

    const handleClickOpen = async () => {
        setOpen(true);
        await GetCharacteristicGroups();
        await GetByIdCharacteristic(id);
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const onHandleSubmit = async (values: ICharacteristic) => {
        try {
            await UpdateCharacteristic(characteristicInfo.id, values);
            await GetCharacteristics();
            handleClickClose();
            resetForm();
        }
        catch (ex) {
            const serverErrors = ex as CharacteristicServerError;
            if (serverErrors.errors)
                Object.entries(serverErrors.errors).forEach(([key, value]) => {
                    if (Array.isArray(value)) {
                        let message = "";
                        value.forEach((item) => {
                            message += `${item} `;
                        });
                        setFieldError(key.toLowerCase(), message);
                    }
                });
        }
    }

    const formik = useFormik({
        initialValues: item,
        validationSchema: validationFields,
        enableReinitialize: true,
        onSubmit: onHandleSubmit
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps,resetForm } = formik;

    return (
        <DialogComponent
            open={open}
            handleClickClose={handleClickClose}
            button={
                <IconButton
                    aria-label="edit"
                    onClick={handleClickOpen}
                >
                    <EditIcon />
                </IconButton>
            }

            formik={formik}
            isSubmitting={isSubmitting}
            handleSubmit={handleSubmit}

            dialogTitle="Update"
            dialogBtnCancel="Close"
            dialogBtnConfirm="Update"

            dialogContent={
                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="Name"
                            error={errors.name}
                            touched={touched.name}
                            getFieldProps={{ ...getFieldProps('name') }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <SelectComponent
                            label="Characteristic group"
                            items={characteristicGroups}
                            error={errors.characteristicGroupId}
                            touched={touched.characteristicGroupId}
                            getFieldProps={{ ...getFieldProps('characteristicGroupId') }}
                        />
                    </Grid>
                </Grid>
            }
        />
    )
}

export default CharacteristicUpdate;