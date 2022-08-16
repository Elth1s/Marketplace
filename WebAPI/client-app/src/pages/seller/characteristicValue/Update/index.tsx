import Grid from '@mui/material/Grid';
import IconButton from '@mui/material/IconButton';
import EditIcon from '@mui/icons-material/Edit';

import { FC, useState } from "react";
import { useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { validationFields } from "../validation";
import { ICharacteristicValue } from "../types";
import { ServerError } from '../../../../store/types';

import DialogComponent from '../../../../components/Dialog';
import TextFieldComponent from '../../../../components/TextField';
import { toLowerFirstLetter } from '../../../../http_comon';
import { Edit } from '@mui/icons-material';
import AutocompleteComponent from '../../../../components/Autocomplete';

interface Props {
    id: number,
    afterUpdate: any
}

const CharacteristicValueUpdate: FC<Props> = ({ id, afterUpdate }) => {
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
    };

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

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps, setFieldValue } = formik;

    return (
        <DialogComponent
            open={open}
            handleClickClose={handleClickClose}
            button={
                <Edit onClick={() => handleClickOpen()} />
            }

            formik={formik}
            isSubmitting={isSubmitting}
            handleSubmit={handleSubmit}

            dialogTitle="Update characteristic value"
            dialogBtnConfirm="Update"

            dialogContent={
                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="Value"
                            error={errors.value}
                            touched={touched.value}
                            getFieldProps={{ ...getFieldProps('value') }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <AutocompleteComponent
                            label="Characteristic name"
                            name="characteristicNameId"
                            error={errors.characteristicNameId}
                            touched={touched.characteristicNameId}
                            options={characteristicNames}
                            getOptionLabel={(option) => option.name}
                            isOptionEqualToValue={(option, value) => option?.id === value.id}
                            defaultValue={characteristicNames.find(value => value.id === selectedCharacteristicValue.characteristicNameId)}
                            onChange={(e, value) => { setFieldValue("characteristicNameId", value?.id) }}
                        />
                    </Grid>
                </Grid>
            }
        />
    )
}

export default CharacteristicValueUpdate;