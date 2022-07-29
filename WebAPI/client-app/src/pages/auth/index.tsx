import {
    Dialog
} from '@mui/material';
import { FC, useState } from 'react'

import { GoogleReCaptchaProvider } from 'react-google-recaptcha-v3';
import ForgotPasswordDialog from './ForgotPassword';
import SignInDialog from './SignInDialog/SignInDialog';
import SignUpDialog from './SignUpDialog/SignUpDialog';

import { DialogStyle } from '../../components/Dialog/styled';

interface Props {
    dialogOpen: boolean,
    dialogClose: any
}

const AuthDialog: FC<Props> = ({ dialogOpen, dialogClose }) => {
    const [isSignInDialog, setIsSignInDialog] = useState<boolean>(true)
    const [forgotPasswordDialogOpen, setForgotPasswordDialogOpen] = useState<boolean>(false);

    const changeDialog = () => {
        setIsSignInDialog(!isSignInDialog)
    }

    const forgotPasswordDialogClose = () => {
        setForgotPasswordDialogOpen(false);
    };

    return (
        <>
            <DialogStyle
                open={dialogOpen}
                onClose={dialogClose}
                aria-describedby="alert-dialog-slide-description"
            >
                <GoogleReCaptchaProvider reCaptchaKey="6LeVndYfAAAAAKY8A3dLGIL06JS5KBAeAqpIzlwX">
                    {isSignInDialog
                        ? <SignInDialog dialogClose={dialogClose} changeDialog={changeDialog} forgotPasswordOpen={() => { setForgotPasswordDialogOpen(true) }} />
                        : <SignUpDialog dialogClose={dialogClose} changeDialog={changeDialog} />}
                </GoogleReCaptchaProvider>
            </DialogStyle>
            <ForgotPasswordDialog dialogOpen={forgotPasswordDialogOpen} dialogClose={forgotPasswordDialogClose} />
        </>
    )
}

export default AuthDialog