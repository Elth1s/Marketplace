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
            sx={{
                display: "flex",
                justifyContent: "space-between",
                alignItems: "center",
                p: "40px"
            }}
        >
            <Typography variant="h1" sx={{ color: "#F45626" }} >
                {title}
            </Typography>
            <IconButton aria-label="close" onClick={onClick}>
                <Close sx={{ color: "#000000", fontSize: "20px" }} />
            </IconButton>
        </DialogTitle>
    )
}

export default DialogTitleWithButton;