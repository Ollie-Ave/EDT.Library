import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
import PrimeVue from 'primevue/config';
import Aura from '@primevue/themes/aura';
import PhosphorIcons from "@phosphor-icons/vue"

const app = createApp(App)

app.use(createPinia())
app.use(router)
app.use(PhosphorIcons)
app.use(PrimeVue, {
    theme: {
        preset: Aura
    }
});

app.mount('#app')
