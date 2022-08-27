import {
    Dialog
} from '@mui/material';
import { FC, useState } from 'react'

import { GoogleReCaptchaProvider } from 'react-google-recaptcha-v3';
import ForgotPasswordDialog from './ForgotPassword';
import SignInDialog from './SignInDialog/SignInDialog';
import SignUpDialog from './SignUpDialog/SignUpDialog';

import { DialogStyle } from '../../components/Dialog/styled';
import { useActions } from '../../hooks/useActions';
import { useTypedSelector } from '../../hooks/useTypedSelector';


const AuthDialog = () => {
    const { AuthDialogChange } = useActions();
    const { isAuthDialogOpen } = useTypedSelector(state => state.auth)

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
                open={isAuthDialogOpen}
                onClose={AuthDialogChange}
                aria-describedby="alert-dialog-slide-description"
            >
                <GoogleReCaptchaProvider reCaptchaKey="6LeVndYfAAAAAKY8A3dLGIL06JS5KBAeAqpIzlwX">
                    {isSignInDialog
                        ? <SignInDialog changeDialog={changeDialog} forgotPasswordOpen={() => { setForgotPasswordDialogOpen(true) }} />
                        : <SignUpDialog changeDialog={changeDialog} />}
                </GoogleReCaptchaProvider>
            </DialogStyle>
            <ForgotPasswordDialog dialogOpen={forgotPasswordDialogOpen} dialogClose={forgotPasswordDialogClose} />
        </>
    )
}

export default AuthDialog