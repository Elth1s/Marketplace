// import {
//     Grid,
// } from "@mui/material";
// import { Edit } from "@mui/icons-material";

// import { useState, FC } from "react";
// import { useFormik } from "formik";
// import { ServerError, UpdateProps } from "../../../../store/types";
// import { useTypedSelector } from "../../../../hooks/useTypedSelector";
// import { useActions } from "../../../../hooks/useActions";
// import { fieldValidation } from "../validation";
// import TextFieldComponent from "../../../../components/TextField";
// import DialogComponent from "../../../../components/Dialog";
// import { toLowerFirstLetter } from "../../../../http_comon";
// import { TextFieldFirstStyle } from "../../../../components/TextField/styled";
// import CropperDialog from "../../../../components/CropperDialog";


// const DeliveryTypeUpdate: FC<UpdateProps> = ({ id, afterUpdate }) => {
//     const [open, setOpen] = useState(false);

//     const { GetDeliveryTypeById, UpdateDeliveryType } = useActions();
//     const { selectedDeliveryType } = useTypedSelector((store) => store.deliveryType);

//     const handleClickOpen = async () => {
//         setOpen(true);
//         await GetDeliveryTypeById(id);
//     };

//     const handleClickClose = () => {
//         setOpen(false);
//     };

//     const onSaveDarkIcon = async (base64: string) => {
//         setFieldValue("darkIcon", base64)
//     };
//     const onSaveLightIcon = async (base64: string) => {
//         setFieldValue("lightIcon", base64)
//     };

//     const formik = useFormik({
//         initialValues: selectedDeliveryType,
//         validationSchema: fieldValidation,
//         enableReinitialize: true,
//         onSubmit: async (values, { setFieldError }) => {
//             try {
//                 await UpdateDeliveryType(id, values);
//                 afterUpdate();
//                 handleClickClose();
//             }
//             catch (ex) {
//                 const serverErrors = ex as ServerError;
//                 if (serverErrors.errors)
//                     Object.entries(serverErrors.errors).forEach(([key, value]) => {
//                         if (Array.isArray(value)) {
//                             let message = "";
//                             value.forEach((item) => {
//                                 message += `${item} `;
//                             });
//                             setFieldError(toLowerFirstLetter(key), message);
//                         }
//                     });
//             }
//         }
//     });

//     const { errors, touched, isSubmitting, handleSubmit, getFieldProps, setFieldValue } = formik;

//     return (

//         <DialogComponent
//             open={open}
//             handleClickClose={handleClickClose}
//             button={
//                 <Edit onClick={() => handleClickOpen()} />
//             }

//             formik={formik}
//             isSubmitting={isSubmitting}
//             handleSubmit={handleSubmit}

//             dialogTitle="Update delivery type"
//             dialogBtnConfirm="Update"

//             dialogContent={
//                 <Grid container spacing={2}>
//                     <Grid item xs={12}>
//                         <TextFieldFirstStyle
//                             fullWidth
//                             autoComplete="englishName"
//                             variant="standard"
//                             type="text"
//                             label="English name"
//                             {...getFieldProps('englishName')}
//                             error={Boolean(touched.englishName && errors.englishName)}
//                             helperText={touched.englishName && errors.englishName}
//                         />
//                     </Grid>
//                     <Grid item xs={12}>
//                         <TextFieldFirstStyle
//                             fullWidth
//                             autoComplete="ukrainianName"
//                             variant="standard"
//                             type="text"
//                             label="Ukrainian name"
//                             {...getFieldProps('ukrainianName')}
//                             error={Boolean(touched.ukrainianName && errors.ukrainianName)}
//                             helperText={touched.ukrainianName && errors.ukrainianName}
//                         />
//                     </Grid>
//                     <Grid item xs={3}>
//                         <CropperDialog
//                             imgSrc={formik.values.lightIcon}
//                             onDialogSave={onSaveLightIcon}
//                             labelId="LightIcon"
//                         />
//                     </Grid>
//                     <Grid item xs={3}>
//                         <CropperDialog
//                             imgSrc={formik.values.darkIcon}
//                             onDialogSave={onSaveDarkIcon}
//                             labelId="DarkIcon"
//                         />
//                     </Grid>
//                 </Grid>
//             }
//         />
//     )
// }
// export default DeliveryTypeUpdate;

import React from 'react'

type Props = {}

const index = (props: Props) => {
    return (
        <div>index</div>
    )
}

export default index