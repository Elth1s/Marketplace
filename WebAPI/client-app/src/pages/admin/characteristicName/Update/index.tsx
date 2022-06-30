import Grid from '@mui/material/Grid';
import IconButton from '@mui/material/IconButton';
import EditIcon from '@mui/icons-material/Edit';

import { FC, useState } from "react";
import { useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { validationFields } from "../validation";
import { ICharacteristicName } from "../types";
import { ServerError } from '../../../../store/types';

import DialogComponent from '../../../../components/Dialog';
import SelectComponent from '../../../../components/Select';
import TextFieldComponent from '../../../../components/TextField';

interface Props {
    id: number,
    afterUpdate: any
}

const CharacteristicUpdate: FC<Props> = ({ id, afterUpdate }) => {
    const [open, setOpen] = useState(false);

    const { GetByIdCharacteristicName, GetCharacteristicGroups, UpdateCharacteristicName, GetCharacteristicNames } = useActions();

    const { characteristicNameInfo } = useTypedSelector((store) => store.characteristicName);
    const { characteristicGroups } = useTypedSelector((store) => store.characteristicGroup);

    const item: ICharacteristicName = {
        name: characteristicNameInfo.name,
        characteristicGroupId: characteristicGroups.find(n => n.name === characteristicNameInfo.characteristicGroupName)?.id || '',
        unitId:  characteristicGroups.find(n => n.name === characteristicNameInfo.unitMeasure)?.id || '',
    }

    const handleClickOpen = async () => {
        setOpen(true);
        await GetCharacteristicGroups();
        await GetByIdCharacteristicName(id);
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const onHandleSubmit = async (values: ICharacteristicName) => {
        try {
            await UpdateCharacteristicName(characteristicNameInfo.id, values);
            await GetCharacteristicNames();
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

            dialogTitle="Update characteristic"
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