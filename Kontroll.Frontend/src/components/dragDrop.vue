<template>
  <div class="drop-zone-wrapper">
    <div v-if="file" class="drop-zone-confirm">
        <div class=".confirm-text">Vil du bruke denne filen?</div>
      <button class="confirm-button" @click="confirmUpload">Last opp</button><br/>
    </div>
    <div
      class="drop-zone"
      @dragover.prevent
      @dragenter.prevent
      @drop.prevent="handleDrop"
    >
      <div v-if="file">
        <div class="icon">📄</div>
        <div class="filename">{{ file.name }}</div>
      </div>
      <div v-else class="placeholder-text">
        <span>📄</span><br/>
        Dra og slipp
        <br/> 
        en CSV-fil her
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const emit = defineEmits(['file-confirmed'])

const file = ref(null)

function handleDrop(event) {
  const droppedFile = event.dataTransfer.files[0]
  if (!droppedFile || droppedFile.type !== 'text/csv') {
    alert('Vennligst slipp en gyldig CSV-fil.')
    return
  }

  file.value = droppedFile
}

function confirmUpload() {
  if (file.value) {
    emit('file-confirmed', file.value) // 👈 send filen opp
  }
}
</script>

<style scoped>

.drop-zone-wrapper {
  display: flex;
  height: 100%; 
  width: 100%;
  align-content: center;
}

.drop-zone-confirm {
  width: 40%;
  height: 60%;
  margin: auto;
  text-align: center;
  align-content: center;
  font-family: sans-serif;
}

.drop-zone {
  width: 40%;
  height: 60%;
  border: 2px solid rgba(var(--bs-header-bg-rgb), 0.8);
  text-align: center;
  align-content: center;
  border-radius: 10px;
  background-color: rgba(var(--bs-header-bg-rgb), 0.3);
  transition: background 0.3s;
  margin: auto;
  font-family: sans-serif;
}

.drop-zone:hover {
  background-color: rgba(var(--bs-content-bg-rgb), 0.9);
  cursor: pointer;
}

.icon {
  font-size: 2rem;
}

.filename {
  font-weight: bold;
  font-size: 0.5rem;
}

.confirm-text {
  margin-top: 1rem;
  font-size: 0.9rem;
}

.confirm-button {
  margin-top: 0.8rem;
  padding: 0.5rem 1rem;
  font-size: 0.9rem;
  border: none;
  background-color: rgba(var(--bs-header-bg-rgb), 1);
  color: rgba(var(--bs-body-color-rgb), 0.8);
  border-radius: 5px;
  cursor: pointer;
  box-sizing: border-box;
}

.confirm-button:hover {
  background-color: rgba(var(--bs-content-bg-rgb), 0.2);
  border: 1px solid rgba(var(--bs-header-bg-rgb), 1);
}

.placeholder-text {
  color: rgba(var(--bs-body-color-rgb), 0.5);
  font-size: 0.9rem;
}
</style>
