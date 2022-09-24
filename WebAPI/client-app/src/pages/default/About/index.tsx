import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";

import { useTranslation } from "react-i18next";

import { about } from "../../../assets/backgrounds";

const About = () => {
    const { t } = useTranslation();

    return (
        <Grid container>
            <Grid item xs={6}>
                <Typography variant="h1" color="inherit">{t('pages.about.aboutUs.title')}</Typography>
                <Typography variant="h2" color="inherit" sx={{ mt: "25px" }}>{t('pages.about.aboutUs.desc')}</Typography>
                <Typography variant="h1" color="inherit" sx={{ mt: "30px" }}>{t('pages.about.ourGoal.title')}</Typography>
                <Typography variant="h2" color="inherit" sx={{ mt: "25px" }}>{t('pages.about.ourGoal.desc')}</Typography>
                <Typography variant="h1" color="inherit" sx={{ mt: "30px" }}>{t('pages.about.convenientDelivery.title')}</Typography>
                <Typography variant="h2" color="inherit" sx={{ mt: "25px" }}>{t('pages.about.convenientDelivery.desc')}</Typography>
                <Typography variant="h1" color="inherit" sx={{ mt: "30px" }}>{t('pages.about.ourTeam.title')}</Typography>
            </Grid>
            <Grid item xs={6}
                sx={{
                    display: "flex",
                    justifyContent: "flex-end",
                    alignItems: "flex-start",
                }}
            >
                <img src={about} alt="image" />
            </Grid>
        </Grid>
    );
}

export default About;