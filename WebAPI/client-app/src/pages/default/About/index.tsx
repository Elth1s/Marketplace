import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";

import { about } from "../../../assets/backgrounds";

const About = () => {
    return (
        <Grid container>
            <Grid item xs={6}>
                <Typography variant="h1">Про нас</Typography>
                <Typography variant="h2" sx={{ mt: "25px" }}>
                    Mall  — найбільший маркетплейс України, де продаються товари від десятків тисяч підприємців з усієї країни.
                    У нас можна знайти буквально все. Надаємо гарантію, бо вважаємо, що онлайн-шопінг має бути максимально безпечним.
                </Typography>
                <Typography variant="h1" sx={{ mt: "30px" }}>Наша мета</Typography>
                <Typography variant="h2" sx={{ mt: "25px" }}>
                    Ми робимо все для того, щоб покупцям було приємно та просто шукати те, що вони захочуть.
                    Адже саме у нас є все! Ми завжди виконуємо бажання наших покупців, і стараємося позбавити від неприємних ситуацій.
                </Typography>
                <Typography variant="h1" sx={{ mt: "30px" }}>Зручна доставка</Typography>
                <Typography variant="h2" sx={{ mt: "25px" }}>
                    Звичайно, будь-який товар можна замовити з доставкою. Доставляємо замовлення протягом одного дня по Києву, по Україні — на наступний день.
                    Все — без передоплати, при необхідності — в кредит. Оплата готівкова або безготівкова, як вам зручніше.
                </Typography>
                <Typography variant="h1" sx={{ mt: "30px" }}>Наша команда - наше все!</Typography>
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