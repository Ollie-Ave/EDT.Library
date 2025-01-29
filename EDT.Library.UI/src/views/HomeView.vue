
<script setup lang="ts">
import Toolbar from 'primevue/toolbar';
import Card from 'primevue/card';
import Button from 'primevue/button';

import CreateBook from '@/components/CreateBook.vue';
import EditBook from '@/components/EditBook.vue';

import { storeToRefs } from 'pinia';
import { useBookStore } from '@/stores/BookStore';
import axios from 'axios';

async function deleteBook(id: number) : Promise<void> {
  try {
    const response = await axios.post(`/api/books/delete/${id}`);

    if (response.status === 200) {
      await getOrRefreshBooks();
    }
    else {
      console.error(response.statusText);
    }
  } catch (error) {
    console.error(error);
  }
}

const { books } = storeToRefs(useBookStore());
const { getOrRefreshBooks } = useBookStore();

await getOrRefreshBooks();
</script>

<template>
  <main class="w-4/5 mx-auto my-8 flex flex-col gap-6">
    <div class="flex flex-col gap-2">
      <h2>Welcome to the</h2>
      <h1>EDT Book Library</h1>
    </div>

  <Toolbar>
      <template #end>
        <CreateBook />
      </template>
    </Toolbar>

    <div class="grid md:grid-cols-2 grid-cols-1 gap-4">
      <Card v-for="book in books" v-bind:key="book.id">
          <template #title>
            <h3>{{ book.title }}</h3>
            <h4 class="!text-base">{{ book.author }} - {{ book.publishedYear }}</h4>
          </template>
          <template #content>
              <div class="flex flex-col gap-4">
                <p>
                  {{ book.description }}
                </p>

                <div class="flex gap-2 justify-between flex-col md:flex-row">
                    <EditBook :id="book.id"
                        :original-author="book.author"
                        :original-description="book.description"
                        :original-published-year="book.publishedYear"
                        :original-title="book.title" />

                      <Button @click="deleteBook(book.id)">Delete</Button>
                </div>
              </div>
          </template>
      </Card>
    </div>
  </main>
</template>


