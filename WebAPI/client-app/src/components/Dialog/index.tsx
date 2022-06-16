import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Button from '@mui/material/Button';

import { Form, FormikProvider } from "formik";

import { IDialog } from "./type";

const DialogComponent: React.FC<IDialog> = ({
    open,
    handleClickClose,
    button,

    formik,
    isSubmitting,
    handleSubmit,

    dialogTitle,
    dialogBtnCancel,
    dialogBtnConfirm,

    dialogContent,
}) => {
    return (
        <>
            { button }
            <Dialog
                open={open}
                maxWidth="sm"
                fullWidth={true}
                onClose={handleClickClose}>
                <DialogTitle>{dialogTitle}</DialogTitle>
                <FormikProvider value={formik} >
                    <Form onSubmit={handleSubmit}>
                        <DialogContent>
                            {dialogContent}
                        </DialogContent>
                        <DialogActions>
                            <Button
                                onClick={handleClickClose}
                            >
                                {dialogBtnCancel}
                            </Button>
                            <Button
                                type="submit"
                                variant="contained"
                                disabled={isSubmitting}
                            >
                                {dialogBtnConfirm}
                            </Button>
                        </DialogActions>
                    </Form>
                </FormikProvider>
            </Dialog>
        </>
    )
}

export default DialogComponent;