import DialogTitle from "@mui/material/DialogTitle"
import Typography from "@mui/material/Typography"
import Box from "@mui/material/Box";

import IconButton from "@mui/material/IconButton"

import { Close } from "@mui/icons-material";

import { FC } from "react";

interface props {
    title: string;
    onClick: () => void;
}

const DialogTitleWithButton: FC<props> = ({ title, onClick }) => {
    return (
        <DialogTitle
            component={Box}
            color="inherit"
            sx={{
                display: "flex",
                justifyContent: "space-between",
                alignItems: "center",
                pl: "26px",
                pr: "16px"
            }}
        >
            <Typography variant="h1" color="inherit">
                {title}
            </Typography>
            <IconButton onClick={onClick}
                color="inherit"
                sx={{
                    borderRadius: "12px"
                }}
            >
                <Close />
            </IconButton>
        </DialogTitle>
    )
}

export default DialogTitleWithButton;