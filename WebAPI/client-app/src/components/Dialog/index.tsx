import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';

import { Form, FormikProvider } from "formik";

import { IconButton, Slide } from '@mui/material';
import { Close } from '@mui/icons-material';
import { LoadingButtonStyled } from './styled';
import { forwardRef } from 'react';

export interface IDialog {
    open: boolean,
    handleClickClose: React.MouseEventHandler<HTMLButtonElement> | undefined,
    button: any,

    formik: any,
    isSubmitting: boolean,
    handleSubmit: (e?: React.FormEvent<HTMLFormElement> | undefined) => void

    dialogTitle: string,
    dialogBtnConfirm: string,

    dialogContent: any,
};

const Transition = forwardRef(function Transition(props: any, ref) {
    return <Slide direction="left" ref={ref} {...props} />;
});

const DialogComponent: React.FC<IDialog> = ({
    open,
    handleClickClose,
    button,

    formik,
    isSubmitting,
    handleSubmit,

    dialogTitle,
    dialogBtnConfirm,

    dialogContent,
}) => {
    return (
        <>
            {button}
            <Dialog
                open={open}
                maxWidth="sm"
                fullWidth={true}
                onClose={handleClickClose}
                TransitionComponent={Transition}
                PaperProps={{
                    sx: {
                        width: {
                            sm: "50rem"
                        }
                    },
                    style: { borderRadius: 12 }
                }}>
                <DialogTitle sx={{ m: 0, px: 3 }}>
                    {dialogTitle}
                    <IconButton
                        aria-label="close"
                        onClick={handleClickClose}
                        sx={{
                            position: 'absolute',
                            my: "auto",
                            right: 8,
                            top: 10,
                            borderRadius: "12px"
                        }}
                    >
                        <Close />
                    </IconButton>
                </DialogTitle>
                <FormikProvider value={formik} >
                    <Form onSubmit={handleSubmit}>
                        <DialogContent sx={{ m: 0, px: 3, pt: 0 }}>
                            {dialogContent}
                        </DialogContent>
                        <DialogActions sx={{ m: 0, p: 3, pt: 0 }}>
                            <LoadingButtonStyled
                                type="submit"
                                loading={isSubmitting}
                                size="large"
                                variant="contained"
                            >
                                {dialogBtnConfirm}
                            </LoadingButtonStyled>
                        </DialogActions>
                    </Form>
                </FormikProvider>
            </Dialog>
        </>
    )
}

export default DialogComponent;