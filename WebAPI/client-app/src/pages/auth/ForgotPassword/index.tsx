import { Dialog } from "@mui/material"
import { FC, useState } from "react"
import { DialogStyle } from "../../../components/Dialog/styled"

import EmailDialog from "./EmailDialog"
import PhoneDialog from "./PhoneDialog"

interface Props {
    dialogOpen: boolean,
    dialogClose: any
}

const ForgotPasswordDialog: FC<Props> = ({ dialogOpen, dialogClose }) => {
    const [isEmailDialog, setIsEmailDialog] = useState<boolean>(true)

    const changeDialog = () => {
        setIsEmailDialog(!isEmailDialog)
    }

    return (
        <DialogStyle
            open={dialogOpen}
            onClose={dialogClose}
            aria-describedby="alert-dialog-slide-description"
        >
            {isEmailDialog
                ? <EmailDialog dialogClose={dialogClose} changeDialog={changeDialog} />
                : <PhoneDialog dialogClose={dialogClose} changeDialog={changeDialog} />}
        </DialogStyle>
    )
}

export default ForgotPasswordDialog;