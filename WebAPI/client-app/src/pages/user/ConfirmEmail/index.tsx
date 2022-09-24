import { Box, CircularProgress, Stack, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { useSearchParams } from "react-router-dom";
import { useActions } from "../../../hooks/useActions";
import { IConfirmEmail } from "../types";


const ConfirmEmail = () => {
    const { t } = useTranslation();
    const { ConfirmEmail } = useActions();

    let [searchParams] = useSearchParams();

    const [loading, setLoading] = useState<boolean>(false);
    const [isSuccess, setIsSuccess] = useState<boolean>(false);

    useEffect(() => {
        async function confirmEmail() {
            setLoading(true);
            try {
                const requestData: IConfirmEmail = {
                    userId: searchParams.get("userId") ?? "",
                    confirmationCode: searchParams.get("token") ?? "",
                }
                await ConfirmEmail(requestData)
                setLoading(false);
                setIsSuccess(true);
            } catch (ex) {
                setLoading(false);
                setIsSuccess(false);
            }
        }
        if (!isSuccess)
            confirmEmail()
    }, [isSuccess])

    return (
        <>
            {loading
                ? <Box sx={{ display: 'flex', justifyContent: 'center' }}>
                    <CircularProgress sx={{ mt: 3 }} />
                </Box>
                : <Box sx={{ display: 'flex', justifyContent: 'center' }}>
                    {isSuccess ? <h1>{t('pages.confirmEmail.success')}</h1> :
                        <h1>{t('pages.confirmEmail.failed')}</h1>}
                </Box>
            }
        </>
    )
}

export default ConfirmEmail;