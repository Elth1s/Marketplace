import Typography from "@mui/material/Typography";
import Box from "@mui/material/Box";
import { useTheme } from "@mui/material"

import { useTranslation } from 'react-i18next';

import {
    outline_shop_85_dark, outline_shop_85_light,
    shopping_bag_85_dark, shopping_bag_85_light
} from "../../../../assets/icons";

const ContactInfo = () => {
    const { t } = useTranslation();
    const { palette } = useTheme();

    return (
        <Box sx={{
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            justifyContent: "space-between",
        }}
        >
            <Box sx={{ display: "flex", justifyContent: "space-between", width: "1188px" }}>
                <Box sx={{ width: "480px" }}>
                    <Box sx={{ display: "flex", alignItems: "center", }}>
                        <img src={palette.mode == "dark" ? outline_shop_85_dark : outline_shop_85_light} alt="icon outline shop" />
                        <Typography variant="h3" color="inherit" sx={{ fontWeight: "600", ml: "30px" }}>{t('pages.contactInfo.sellers.title')}</Typography>
                    </Box>
                    <Typography variant="h5" color="inherit" sx={{ fontWeight: "500", mt: "35px" }}>
                        {t('pages.contactInfo.sellers.description')}
                        <Typography color="inherit" sx={{ fontWeight: "700" }}>{t('pages.contactInfo.sellers.date')}</Typography>
                        <Typography color="inherit">{t('pages.contactInfo.sellers.work')}</Typography>
                        <Typography color="inherit">{t('pages.contactInfo.sellers.weekend')}</Typography>
                    </Typography>
                </Box>
                <Box sx={{ width: "480px" }}>
                    <Box sx={{ display: "flex", alignItems: "center", }}>
                        <img src={palette.mode == "dark" ? shopping_bag_85_dark : shopping_bag_85_light} alt="icon shopping bag" />
                        <Typography variant="h3" color="inherit" sx={{ fontWeight: "600", ml: "30px" }}>{t('pages.contactInfo.buyers.title')}</Typography>
                    </Box>
                    <Typography variant="h5" color="inherit" sx={{ mt: "35px" }}>
                        {t('pages.contactInfo.buyers.description')}
                    </Typography>
                </Box>
            </Box>
            <Box sx={{ width: "1188px", mt: "100px" }}>
                <Typography variant="h5" color="inherit" sx={{ fontWeight: "500" }}>
                    {t('pages.contactInfo.description')}
                    <Typography variant="h5" color="inherit" sx={{ fontWeight: "700" }}>+380 (73) 678 32 40</Typography>
                    <Typography variant="h5" color="inherit">
                        E-mail:
                        <Typography variant="h5" color="inherit" sx={{ display: "inline", fontWeight: "700" }}>
                            info@comfy.ua
                        </Typography>
                    </Typography>
                </Typography>
            </Box>
        </Box>
    );
}

export default ContactInfo;