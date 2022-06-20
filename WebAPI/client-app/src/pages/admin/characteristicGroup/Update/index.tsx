import Grid from '@mui/material/Grid';
import IconButton from '@mui/material/IconButton';
import EditIcon from '@mui/icons-material/Edit';

import { FC, useState } from "react";
import { useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { validationFields } from "../validation";
import { ICharacteristicGroup, ICharacteristicGroupUpdatePage } from "../types";
import { ServerError } from '../../../../store/types';

import DialogComponent from '../../../../components/Dialog';
import TextFieldComponent from '../../../../components/TextField';

const CharacteristicGroupUpdate: FC<ICharacteristicGroupUpdatePage> = ({ id }) => {
    const [open, setOpen] = useState(false);

    const { GetCharacteristicGroups, GetByIdCharacteristicGroup, UpdateCharacteristicGroup } = useActions();
    const { characteristicGroupInfo } = useTypedSelector((store) => store.characteristicGroup);

    const item: ICharacteristicGroup = {
        name: characteristicGroupInfo.name,
    }

    const handleClickOpen = async () => {
        setOpen(true);
        await GetByIdCharacteristicGroup(id);
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const onHandleSubmit = async (values: ICharacteristicGroup) => {
        try {
            await UpdateCharacteristicGroup(characteristicGroupInfo.id, values);
            await GetCharacteristicGroups();
            handleClickClose();
            resetForm();
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

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps, resetForm } = formik;

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

            dialogTitle="Create"
            dialogBtnCancel="Close"
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
                </Grid>
            }
        />
    )
}

export default CharacteristicGroupUpdate;