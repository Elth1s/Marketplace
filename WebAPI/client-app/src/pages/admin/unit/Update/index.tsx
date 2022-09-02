import Grid from '@mui/material/Grid';
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

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from '../../../../hooks/useTypedSelector';

import { UpdateProps, ServerError, } from '../../../../store/types';

import { toLowerFirstLetter } from '../../../../http_comon';


const Transition = forwardRef(function Transition(
    props: TransitionProps & {
        children: React.ReactElement<any, any>;
    },
    ref: React.Ref<unknown>,
) {
    return <Slide direction="left" ref={ref} {...props} />;
});

const Update: FC<UpdateProps> = ({ id, afterUpdate }) => {
    const { t } = useTranslation();

    const [open, setOpen] = useState(false);

    const { GetUnitById, UpdateUnit } = useActions();
    const { selectedUnit } = useTypedSelector((store) => store.unit);

    const handleClickOpen = async () => {
        setOpen(true);
        await GetUnitById(id);
    };

    const handleClickClose = () => {
        setOpen(false);
        resetForm();
    };

    const UnitSchema = Yup.object().shape({
        englishMeasure: Yup.string().min(1).max(30).required().label(t('validationProps.englishMeasure')),
        ukrainianMeasure: Yup.string().min(1).max(30).required().label(t('validationProps.ukrainianMeasure')),
    });

    const formik = useFormik({
        initialValues: selectedUnit,
        validationSchema: UnitSchema,
        enableReinitialize: true,
        onSubmit: async (values, { setFieldError }) => {
            try {
                await UpdateUnit(id, values);
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
    });

    const { errors, touched, isSubmitting, handleSubmit, getFieldProps, resetForm } = formik;

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
                    title={t('pages.admin.unit.updateTitle')}
                    onClick={handleClickClose}
                />
                <FormikProvider value={formik} >
                    <Form onSubmit={handleSubmit}>
                        <DialogContent sx={{ padding: "10px 40px 45px" }}>
                            <Grid container rowSpacing={5.25}>
                                <Grid item xs={12}>
                                    <TextFieldFirstStyle
                                        fullWidth
                                        autoComplete="englishMeasure"
                                        variant="standard"
                                        type="text"
                                        label={t('validationProps.englishName')}
                                        {...getFieldProps('englishMeasure')}
                                        error={Boolean(touched.englishMeasure && errors.englishMeasure)}
                                        helperText={touched.englishMeasure && errors.englishMeasure}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <TextFieldFirstStyle
                                        fullWidth
                                        autoComplete="ukrainianMeasure"
                                        variant="standard"
                                        type="text"
                                        label={t('validationProps.ukrainianName')}
                                        {...getFieldProps('ukrainianMeasure')}
                                        error={Boolean(touched.ukrainianMeasure && errors.ukrainianMeasure)}
                                        helperText={touched.ukrainianMeasure && errors.ukrainianMeasure}
                                    />
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

export default Update;