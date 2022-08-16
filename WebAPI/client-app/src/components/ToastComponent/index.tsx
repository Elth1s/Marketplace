import { Box, IconButton, Typography } from '@mui/material';
import { FC } from 'react'
import { BoxIconStyle } from './styled';

import { success, error, warning, info, close } from "../../assets/icons"


interface MainToastProps {
    background: string,
    icon: string,
    title: string,
    message: string,
}

interface Props {
    title: string,
    message: string
}

const ToastComponent: FC<MainToastProps> = ({ background, icon, title, message }) => {
    return (
        <Box sx={{ display: "flex", alignItems: "center", height: "100%" }}>
            <BoxIconStyle iconBackground={background} sx={{ display: "flex", justifyContent: "center", alignItems: "center" }}>
                <img
                    style={{ width: "30px", height: "30px" }}
                    src={icon}
                    alt="icon"
                />
            </BoxIconStyle>
            <Box sx={{ height: "100%", display: "flex", flexDirection: "column", justifyContent: "space-between", py: "12px", pl: "11px" }}>
                <Typography variant="h5" color="common.black">
                    {title}
                </Typography>
                <Typography variant="subtitle1" color="common.black">
                    {message}
                </Typography>
            </Box>
            <IconButton
                sx={{ ml: "auto", "&:hover": { background: "transparent" }, "&& .MuiTouchRipple-child": { backgroundColor: "transparent" } }}
                color="inherit"
            >
                <img
                    style={{ width: "20px", height: "20px" }}
                    src={close}
                    alt="icon"
                />
            </IconButton>
        </Box>
    )
}

export const ToastSuccess: FC<Props> = ({ title, message }) => {
    return (
        <ToastComponent background="#A7FFCA" icon={success} title={title} message={message} />
    )
}

export const ToastError: FC<Props> = ({ title, message }) => {
    return (
        <ToastComponent background="#FFA7A7" icon={error} title={title} message={message} />
    )
}

export const ToastWarning: FC<Props> = ({ title, message }) => {
    return (
        <ToastComponent background="#FFAE94" icon={warning} title={title} message={message} />
    )
}

export const ToastInfo: FC<Props> = ({ title, message }) => {
    return (
        <ToastComponent background="#74BAFF" icon={info} title={title} message={message} />
    )
}


