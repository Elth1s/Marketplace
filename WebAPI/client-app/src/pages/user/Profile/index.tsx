import {
    Box,
    Button,
    Grid,
    Stack,
    Typography,
    CircularProgress,
    TextField
} from "@mui/material";
import { LoadingButton } from "@mui/lab";
import { useEffect, useState } from "react";
import { Form, FormikProvider, useFormik } from "formik";
// import { toast } from "react-toastify";

import { useActions } from "../../../hooks/useActions";
import { useTypedSelector } from "../../../hooks/useTypedSelector";
import { ProfileSchema } from "../validation";
import { ServerError } from "../../../store/types";

import CropperDialog from "../../../components/CropperDialog";


const Profile = () => {

    const { GetProfile, UpdateProfile, SendConfirmEmail, IsEmailConfirmed } = useActions();
    const [loading, setLoading] = useState<boolean>(false);


    const { userInfo } = useTypedSelector((store) => store.profile);
    useEffect(() => {
        async function getProfile() {
            setLoading(true);
            try {
                document.title = "Profile";
                await GetProfile();
                await IsEmailConfirmed()
                console.log("1")
                setLoading(false);
            } catch (ex) {
                // toast.error("Loading profile failed.");
                setLoading(false);
            }
        }
        getProfile();
    }, []);

    const formik = useFormik({
        initialValues: userInfo,
        validationSchema: ProfileSchema,
        enableReinitialize: true,
        onSubmit: async (values, { setFieldError }) => {
            try {

                await UpdateProfile(values);
                // toast.success('Update success!');
            }
            catch (exeption) {
                const serverErrors = exeption as ServerError;
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
                let message = "Update failed! \n";
                if (serverErrors.status === 400)
                    message += "Validation failed.";
                // toast.error(message);
            }

        }
    });

    const onSave = async (base64: string) => {
        setFieldValue("photo", base64)
    };
    const onClickConfirmEmailBtn = async () => {
        await SendConfirmEmail();
    };

    const { errors, touched, isSubmitting, handleSubmit, getFieldProps, setFieldValue } = formik;

    return (
        <Box sx={{ flexGrow: 1, m: 1, mx: 3, }}>
            <Stack direction="row" justifyContent="space-between" alignItems="center" sx={{ py: 1 }}>
                <Typography variant="h4" gutterBottom sx={{ my: "auto" }}>
                    Profile info
                </Typography>
            </Stack>
            {loading ? <Box sx={{ display: 'flex', justifyContent: 'center' }}>
                <CircularProgress sx={{ color: "#66fcf1", mt: 3 }} />
            </Box> :
                <>
                    <Box sx={{ mt: 3 }} >
                        <FormikProvider value={formik} >
                            <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
                                <Stack direction="row">
                                    <Grid container spacing={4} sx={{ width: "70%" }}>
                                        <Grid item xs={12} md={6}>
                                            <TextField
                                                fullWidth
                                                autoComplete="firstName"
                                                type="text"
                                                label="First Name"
                                                {...getFieldProps('firstName')}
                                                error={Boolean(touched.firstName && errors.firstName)}
                                                helperText={touched.firstName && errors.firstName}
                                            />
                                        </Grid>
                                        <Grid item xs={12} md={6}>
                                            <TextField
                                                fullWidth
                                                autoComplete="secondName"
                                                type="text"
                                                label="Second Name"
                                                {...getFieldProps('secondName')}
                                                error={Boolean(touched.secondName && errors.secondName)}
                                                helperText={touched.secondName && errors.secondName}
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <TextField
                                                fullWidth
                                                autoComplete="userName"
                                                type="text"
                                                label="Username"
                                                {...getFieldProps('userName')}
                                                error={Boolean(touched.userName && errors.userName)}
                                                helperText={touched.userName && errors.userName}
                                            />
                                        </Grid>
                                        {!userInfo.isEmailConfirmed &&
                                            <>
                                                <Grid item xs={12} md={6}>
                                                    <TextField
                                                        fullWidth
                                                        autoComplete="email"
                                                        type="text"
                                                        label="Email"
                                                        disabled={true}
                                                        {...getFieldProps('email')}
                                                    />
                                                </Grid>
                                                <Grid item xs={12} md={6} display="flex" justifyContent="space-between">
                                                    <Button
                                                        sx={{ alignSelf: "center" }}
                                                        size="large"
                                                        variant="contained" onClick={onClickConfirmEmailBtn}>
                                                        Confirm email
                                                    </Button>
                                                </Grid>
                                            </>
                                        }
                                        <Grid item xs={12} mt={3} display="flex" justifyContent="space-between" >
                                            <LoadingButton
                                                sx={{ paddingX: "35px" }}
                                                size="large"
                                                type="submit"
                                                variant="contained"
                                                loading={isSubmitting}
                                            >
                                                Update
                                            </LoadingButton>
                                        </Grid>
                                    </Grid>
                                    <Grid container sx={{ display: 'flex', justifyContent: 'center', width: "30%" }} >
                                        <CropperDialog
                                            imgSrc={(formik.values.photo === null || formik.values.photo === "") ? "https://www.phoca.cz/images/projects/phoca-download-r.png" : formik.values.photo}
                                            onDialogSave={onSave}
                                        />
                                    </Grid>
                                </Stack>

                            </Form>
                        </FormikProvider>
                    </Box>
                </>
            }
        </Box>
    )
}

export default Profile;