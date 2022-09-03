import { LoadingButton } from "@mui/lab";
import { Dialog, DialogActions, DialogContent, styled } from "@mui/material";


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

export const AdminSellerDialogStyle = styled(Dialog)(({ theme }) => ({
    "& .MuiDialog-paper": {
        minWidth: "700px",
        borderRadius: "10px"
    },
}));

export const AdminSellerDialogContentStyle = styled(DialogContent)(({ theme }) => ({
    paddingLeft: "26px",
    paddingRight: "26px",
}));

export const AdminSellerDialogActionsStyle = styled(DialogActions)(({ theme }) => ({
    paddingBottom: "16px",
    paddingRight: "26px",
}));

export const ReviewQustionDialogStyle = styled(Dialog)(({ theme }) => ({
    "& .MuiDialog-paper": {
        minWidth: "750px",
        borderRadius: "10px"
    },
}));