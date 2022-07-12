import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";

import { useEffect } from "react";

import { Form, FormikProvider, useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import { ServerError } from '../../../../store/types';
import { toLowerFirstLetter } from '../../../../http_comon';

import { ProfileSchema } from "../../validation";
import { IProfile } from "../../types";

import TextFieldComponent from "../../../../components/TextField";

const Change = () => {
    const { GetProfile, UpdateProfile } = useActions();
    const { userInfo } = useTypedSelector((store) => store.profile);

    useEffect(() => {
        getData();
    }, []);

    const getData = async () => {
        document.title = "Change password and login";
        await GetProfile();
    }

    const onHandleSubmit = async (values: IProfile) => {
        try {
            await UpdateProfile(values);
        } catch (ex) {
            const serverErrors = ex as ServerError;
            if (serverErrors.errors) {
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
    }

    const formik = useFormik({
        initialValues: userInfo,
        validationSchema: ProfileSchema,
        enableReinitialize: true,
        onSubmit: onHandleSubmit
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps } = formik;

    return (
        <FormikProvider value={formik} >
            <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
                <Grid container columnSpacing={12}>
                    <Grid container item xs={6} rowSpacing={2.5} direction="column">
                        <Grid container item xs columnSpacing={12.5} direction="row">
                            <Grid item xs={6}>
                                <TextFieldComponent
                                    type="text"
                                    label="Email"
                                    // error={errors.email}
                                    // touched={touched.email}
                                    getFieldProps={{ ...getFieldProps('email') }}
                                />
                            </Grid>
                            <Grid item xs={6}>
                                <TextFieldComponent
                                    type="text"
                                    label="Phone"
                                    // error={errors.phone}
                                    // touched={touched.phone}
                                    getFieldProps={{ ...getFieldProps('phone') }}
                                />
                            </Grid>
                        </Grid>
                        <Grid item xs>
                            <TextFieldComponent
                                type="text"
                                label="Old password"
                                // error={errors.oldPassword}
                                // touched={touched.oldPassword}
                                getFieldProps={{ ...getFieldProps('oldPassword') }}
                            />
                        </Grid>
                        <Grid item xs>
                            <TextFieldComponent
                                type="text"
                                label="New password"
                                // error={errors.newPassword}
                                // touched={touched.newPassword}
                                getFieldProps={{ ...getFieldProps('newPassword') }}
                            />
                        </Grid>
                        <Grid item xs>
                            <TextFieldComponent
                                type="text"
                                label="Confirm password"
                                // error={errors.confirmPassword}
                                // touched={touched.confirmPassword}
                                getFieldProps={{ ...getFieldProps('confirmPassword') }}
                            />
                        </Grid>
                        <Grid item xs>
                            <Typography>Пароль має бути не менше 6 символів, містити цифри та великі літери і не повинен збігатися з ім'ям та ел.поштою</Typography>
                        </Grid>
                        <Button
                            fullWidth
                            variant="contained"
                            disabled={isSubmitting}
                            sx={{ mt: "45px" }}>
                            Save
                        </Button>
                    </Grid>
                    <Grid container item xs={6} rowSpacing={2.5}
                        alignContent="flex-start"
                        direction="column">
                        <Grid item xs>
                            <Typography>Ви можете зв'язати свій особистий кабінет з обліковими записами соціальних мереж, щоб надалі входити на сайт, як користувач Facebook або Google.</Typography>
                        </Grid>
                        <Grid item xs>
                            <TextFieldComponent
                                type="text"
                                label="Google"
                                // error={errors.confirmPassword}
                                // touched={touched.confirmPassword}
                                getFieldProps={{ ...getFieldProps('google') }}
                            />
                        </Grid>
                        <Grid item xs>
                            <TextFieldComponent
                                type="text"
                                label="Facebok"
                                // error={errors.confirmPassword}
                                // touched={touched.confirmPassword}
                                getFieldProps={{ ...getFieldProps('facebok') }}
                            />
                        </Grid>
                        <Button
                            fullWidth
                            variant="outlined"
                            color="secondary"
                            sx={{ mt: "159px" }} >
                            Save
                        </Button>
                    </Grid>
                </Grid>
            </Form>
        </FormikProvider>
    );
}

export default Change;