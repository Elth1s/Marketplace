import { LoadingButton } from "@mui/lab";
import { Dialog, styled } from "@mui/material";


export const LoadingButtonStyle = styled(LoadingButton)(({ theme }) => ({
    backgroundColor: `${theme.palette.secondary.main}`,
    "&:hover": {
        backgroundColor: `${theme.palette.secondary.dark}`,
    },
}));

export const DialogStyle = styled(Dialog)(({ theme }) => ({
    "& .MuiDialog-paper": {
        minWidth: "600px",
        borderRadius: "10px"
    },
}));

export const ReviewDialogStyle = styled(Dialog)(({ theme }) => ({
    "& .MuiDialog-paper": {
        minWidth: "725px",
        borderRadius: "10px"
    },
}));