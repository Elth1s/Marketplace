import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';

import { useState } from "react";
import { useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { validationFields } from "../validation";
import { ICharacteristic } from "../types";
import { ServerError } from '../../../../store/types';

import DialogComponent from '../../../../components/Dialog';
import SelectComponent from "../../../../components/Select";
import TextFieldComponent from "../../../../components/TextField";

const CharacteristicCreate = () => {
    const [open, setOpen] = useState(false);

    const { GetCharacteristics, CreateCharacteristic, GetCharacteristicGroups } = useActions();

    const { characteristicGroups } = useTypedSelector((store) => store.characteristicGroup);

    const item: ICharacteristic = {
        name: '',
        characteristicGroupId: ''
    }

    const handleClickOpen = async () => {
        await GetCharacteristicGroups();

        setOpen(true);
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const onHandleSubmit = async (values: ICharacteristic) => {
        try {
            await CreateCharacteristic(values);
            await GetCharacteristics();
            handleClickClose();
            resetForm();
        } catch (ex) {
            const serverErrors = ex as ServerError;
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
        onSubmit: onHandleSubmit
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps, resetForm } = formik;

    return (
        <DialogComponent
            open={open}
            handleClickClose={handleClickClose}
            button={
                <Button
                    variant="contained"
                    sx={{
                        my: 2,
                        px: 4,
                    }}
                    onClick={handleClickOpen}
                >
                    Create
                </Button>
            }

            formik={formik}
            isSubmitting={isSubmitting}
            handleSubmit={handleSubmit}

            dialogTitle="Create characteristic"
            dialogBtnConfirm="Create"

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

export default CharacteristicCreate;