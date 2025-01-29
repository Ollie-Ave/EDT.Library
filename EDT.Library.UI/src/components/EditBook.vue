<script setup lang="ts">
import Drawer from 'primevue/drawer';
import Button from 'primevue/button';
import InputText from 'primevue/inputtext';
import InputNumber from 'primevue/inputnumber';
import Textarea from 'primevue/textarea';
import FloatLabel from 'primevue/floatlabel';
import { computed, ref } from 'vue';
import axios from 'axios';
import { useBookStore } from '@/stores/BookStore';

const visible = ref<boolean>(false);

const {
	id,
	originalTitle,
	originalAuthor,
	originalDescription,
	originalPublishedYear,
} = defineProps<{
	id: number,
	originalTitle: string,
	originalAuthor: string,
	originalDescription: string,
	originalPublishedYear: number,
}>();

const title = ref<string>(originalTitle);
const author = ref<string>(originalAuthor);
const description = ref<string>(originalDescription);
const publishedYear = ref<number>(originalPublishedYear);

const { getOrRefreshBooks } = useBookStore();

async function submit() {
  try {
    const response = await axios.post('/api/books/update', {
		id,
		title: title.value,
		author: author.value,
		description: description.value,
		publishedYear: publishedYear.value,
	});

    if (response.status === 200) {
		visible.value = false;

		await getOrRefreshBooks();
    }
    else {
      console.error(response.statusText);
    }
  } catch (error) {
    console.error(error);
  }
}

const submitEnabled = computed<boolean>(() => {
  return title.value.length > 0
	&& author.value.length > 0
	&& description.value.length > 0
	&& publishedYear.value !== null;
});
</script>

<template>
	<Drawer v-model:visible="visible" header="Edit Book">
		<div class="flex flex-col gap-2 pt-2">
			<FloatLabel variant="on">
				<InputText v-model="title" type="text" class="!w-full"/>
				<label for="title">Title</label>
			</FloatLabel>

			<FloatLabel variant="on">
				<InputText v-model="author" type="text" class="!w-full" />
				<label for="author">Author</label>
			</FloatLabel>

			<FloatLabel variant="on">
				<InputNumber v-model="publishedYear" class="!w-full" />
				<label for="publishedYear">Year Of Publishing</label>
			</FloatLabel>

			<FloatLabel variant="on">
				<Textarea v-model="description" rows="5" cols="30" class="!w-full"></Textarea>
				<label for="description">Description</label>
			</FloatLabel>

			<Button @click="submit" :disabled="!submitEnabled">Edit Book</Button>
		</div>
	</Drawer>

	<Button @click="visible = true">Edit Book</Button>
</template>